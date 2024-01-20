using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NodaTime;

namespace IMS.Application.Repositories;

public interface IAuthenticationRepository
{
    Task<ResetPasswordDto> CreateResetPassword(ResetPasswordDto resetPassword);
    Task<ResetPasswordToValidateDto> GetResetPasswordDetailsToValidate(Guid id, string resetKey);
    void ResetPasswordMarkAsUsed(Guid resetId);
    Task<LoginSessionDto> CreateUserSession(LoginSessionDto loginSession);
    Task<int> UpdateLoginSession_E(Guid sessionId);
}

public class AuthenticationRepository(ImsContext context,IStringLocalizer<GlobalResource> globalResource) : IAuthenticationRepository
{
    private readonly ImsContext _imsContext = context;
    private IStringLocalizer<GlobalResource> _stringLocalizer = globalResource;

    public async Task<ResetPasswordDto> CreateResetPassword(ResetPasswordDto resetPassword)
    {
        var toSave = resetPassword.Adapt<TblResetPassword>();
        var result = await _imsContext.TblResetPasswords.AddAsync(toSave);
        return result.Entity.Adapt<ResetPasswordDto>();
    }

    public async Task<ResetPasswordToValidateDto> GetResetPasswordDetailsToValidate(Guid id, string resetKey)
    {
        var data = await (from reset in _imsContext.TblResetPasswords
            //join email in _imsContext.TblEmails on reset.Id equals email.ReferenceId
            where reset.Id == id && reset.ResetKey == resetKey
            select new ResetPasswordToValidateDto(reset.Validity, reset.TimestampSend.GetValueOrDefault(), reset.IsUsed, reset.UserId)).SingleOrDefaultAsync();
        if (data == null)
            throw new RecordNotFoundException(_stringLocalizer["RecordNotFound"]);
        return data;
    }

    public void ResetPasswordMarkAsUsed(Guid resetId)
    {
        var toUpdate = new TblResetPassword() { Id = resetId};
        var entry = _imsContext.TblResetPasswords.Attach(toUpdate);
        toUpdate.IsUsed = true;
        entry.PrepareUpdate();
    }

    public async Task<LoginSessionDto> CreateUserSession(LoginSessionDto loginSession)
    {
        var toSave = loginSession.Adapt<TblLoginSession>();
        var result = await _imsContext.TblLoginSessions.AddAsync(toSave);
        return result.Entity.Adapt<LoginSessionDto>();
    }

    public async Task<int> UpdateLoginSession_E(Guid sessionId)
    {
        return await _imsContext.TblLoginSessions
            .Where(o => o.Id == sessionId && o.TimestampEnd > SystemClock.Instance.GetCurrentInstant())
            .ExecuteUpdateAsync(o =>
            o.SetProperty(p => p.TimestampLatestActivity, SystemClock.Instance.GetCurrentInstant()));
    }
}