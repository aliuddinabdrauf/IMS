using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;

namespace IMS.Application.Repositories;

public interface IEmailRepository
{
    Task<EmailDto> CreateNewEmail(EmailDto email);
}

public class EmailRepository(ImsContext imsContext) : IEmailRepository
{
    private readonly ImsContext _imsContext = imsContext;

    public async Task<EmailDto> CreateNewEmail(EmailDto email)
    {
        var toSave = email.Adapt<TblEmail>();
        var result = await _imsContext.TblEmails.AddAsync(toSave);
        return result.Adapt<EmailDto>();
    }
}