using IMS.Application.UoW;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IMS.Application.Services;

public interface IStudentService
{
    Task<StudentComprehensiveDetailsDto> GetStudentDetailsById(Guid id);
}

public class StudentService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService, IEmailService emailService) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IEmailService _emailService = emailService;

    public async Task<StudentComprehensiveDetailsDto> GetStudentDetailsById(Guid id)
    {
        var result = await _unitOfWork.StudentRepository.GetStudentDetailsById(id);
        return result;
    }
}