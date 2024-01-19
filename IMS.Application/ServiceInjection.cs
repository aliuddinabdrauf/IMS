using IMS.Application.Repositories;
using IMS.Application.Services;
using IMS.Application.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace IMS.Application;

public static class ServiceInjection
{
    public static void InjectApplicationServices(this IServiceCollection services)
    {
        #region Repository
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        #endregion

        #region servies

        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IFacultyService, FacultyService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        #endregion
    }
}