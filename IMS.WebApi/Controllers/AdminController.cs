using IMS.Application.Services;
using IMS.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController(IStudentService studentService, IFacultyService facultyService, ICourseService courseService) : ControllerBase
{

    
}