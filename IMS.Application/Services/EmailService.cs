using IMS.Application.UoW;
using IMS.Infrastructure.Dto;

namespace IMS.Application.Services;

public interface IEmailServices
{
    Task<EmailDto> CreateEmail(EmailDto email, bool autoSave = true);
}

public class EmailService(IUnitOfWork unitOfWork) : IEmailServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<EmailDto> CreateEmail(EmailDto email, bool autoSave = true)
    {
        var result = await _unitOfWork.EmailRepositories.CreateNewEmail(email);
        if(autoSave)
            await _unitOfWork.CompleteAsync();
        return result;
    }
}