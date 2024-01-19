using IMS.Application.UoW;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IMS.Application.Services;

public interface IStudentServices
{
    Task<StudentComprehensiveDetailsDto> GetStudentDetailsById(Guid id);
}

public class StudentService(IUnitOfWork unitOfWork, IAuthenticationServices authenticationServices, IEmailServices emailServices) : IStudentServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationServices _authenticationServices = authenticationServices;
    private readonly IEmailServices _emailServices = emailServices;

    public async Task<StudentComprehensiveDetailsDto> GetStudentDetailsById(Guid id)
    {
        var result = await _unitOfWork.StudentRepositories.GetStudentDetailsById(id);
        return result;
    }
}