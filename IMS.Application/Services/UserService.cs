using IMS.Application.UoW;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;

namespace IMS.Application.Services;

public interface IUserServices
{
    Task<StudentDto> CreateUserAsStudent(CreateUserAsStudentDto studentUser);
}

public class UserService(IUnitOfWork unitOfWork, IAuthenticationServices authenticationServices, IEmailServices emailServices) : IUserServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationServices _authenticationServices = authenticationServices;
    private readonly IEmailServices _emailServices = emailServices;
    
    public async Task<StudentDto> CreateUserAsStudent(CreateUserAsStudentDto studentUser)
    {
        var newUser = studentUser.Adapt<UserDto>();
        newUser.Roles = [UserRole.User];
        newUser.Type = AccountType.Student;
        newUser.Status = UserStatus.NeedActivation;
        var user = await _unitOfWork.UserRepositories.CreateUser(newUser);
        var newStudent = studentUser.Adapt<StudentDto>();
        newStudent.UserId = user.Id.GetValueOrDefault();
        var student = await _unitOfWork.StudentRepositories.CreateStudent(newStudent);
        await _unitOfWork.StudentRepositories.MapStudentToCourses(student.Id.GetValueOrDefault(), studentUser.CoursesIds);
        var resetPass = await _authenticationServices.GenerateResetPassword(newStudent.UserId, true);
        var emailDto = new EmailDto("realsender", [], [], [], "subject", "body", "student register", resetPass.Id);
        await _emailServices.CreateEmail(emailDto);
        await _unitOfWork.CompleteAsync();
        return student;
    }
}