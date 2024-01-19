using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NodaTime;

namespace IMS.Application.Repositories;

public interface IAuthenticationRepositories
{
    Task<ResetPasswordDto> CreateResetPassword(ResetPasswordDto resetPassword);
    Task<ResetPasswordToValidateDto> GetResetPasswordDetailsToValidate(Guid id, string resetKey);
}

public class AuthenticationRepository(ImsContext context,IStringLocalizer<GlobalResource> globalResource) : IAuthenticationRepositories
{
    private readonly ImsContext _imsContext = context;
    private IStringLocalizer<GlobalResource> _stringLocalizer = globalResource;

    public async Task<ResetPasswordDto> CreateResetPassword(ResetPasswordDto resetPassword)
    {
        var toSave = resetPassword.Adapt<TblResetPassword>();
        var result = await _imsContext.TblResetPasswords.AddAsync(toSave);
        return result.Adapt<ResetPasswordDto>();
    }

    public async Task<ResetPasswordToValidateDto> GetResetPasswordDetailsToValidate(Guid id, string resetKey)
    {
        var data = await (from reset in _imsContext.TblResetPasswords
            join email in _imsContext.TblEmails on reset.Id equals email.ReferenceId
            where reset.Id == id && reset.ResetKey == resetKey
            select new ResetPasswordToValidateDto(reset.Validity, reset.TimestampSend.GetValueOrDefault())).SingleOrDefaultAsync();
        if (data == null)
            throw new RecordNotFoundException(_stringLocalizer["RecordNotFound"]);
        return data;
    }
}