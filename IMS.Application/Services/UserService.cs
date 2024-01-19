using IMS.Application.UoW;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;

namespace IMS.Application.Services;

public interface IUserService
{
    Task<StudentDto> CreateUserAsStudent(CreateUserAsStudentDto studentUser);
    Task RetrieveByAccountTypeId(UserDto user);
}

public class UserService(IUnitOfWork unitOfWork, IAuthenticationService authenticationService, IEmailService emailService) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthenticationService _authenticationService = authenticationService;
    private readonly IEmailService _emailService = emailService;
    
    public async Task<StudentDto> CreateUserAsStudent(CreateUserAsStudentDto studentUser)
    {
        var newUser = studentUser.Adapt<UserDto>();
        newUser.Roles = [UserRole.User];
        newUser.Type = AccountType.Student;
        newUser.Status = UserStatus.NeedActivation;
        var user = await _unitOfWork.UserRepository.CreateUser(newUser);
        var newStudent = studentUser.Adapt<StudentDto>();
        newStudent.UserId = user.Id.GetValueOrDefault();
        var student = await _unitOfWork.StudentRepository.CreateStudent(newStudent);
        await _unitOfWork.StudentRepository.MapStudentToCourses(student.Id.GetValueOrDefault(), studentUser.CoursesIds);
        var resetPass = await _authenticationService.GenerateResetPassword(newStudent.UserId, true);
        var emailDto = new EmailDto("realsender", [], [], [], "subject", "body", "student register", resetPass.Id);
        await _emailService.CreateEmail(emailDto);
        await _unitOfWork.CompleteAsync();
        return student;
    }

    public async Task RetrieveByAccountTypeId(UserDto user)
    {
        if (user.Type == AccountType.Student)
        {
            user.ByAccountTypeId = await _unitOfWork.StudentRepository.GetStudentIdByUserId(user.Id.GetValueOrDefault());
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}