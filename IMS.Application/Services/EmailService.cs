using IMS.Application.UoW;
using IMS.Infrastructure.Dto;

namespace IMS.Application.Services;

public interface IEmailService
{
    Task<EmailDto> CreateEmail(EmailDto email, bool autoSave = true);
}

public class EmailService(IUnitOfWork unitOfWork) : IEmailService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<EmailDto> CreateEmail(EmailDto email, bool autoSave = true)
    {
        var result = await _unitOfWork.EmailRepository.CreateNewEmail(email);
        if(autoSave)
            await _unitOfWork.CompleteAsync();
        return result;
    }
}