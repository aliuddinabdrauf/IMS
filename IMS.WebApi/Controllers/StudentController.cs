using IMS.Application.Services;
using IMS.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;
[ApiController]
[Route("api/student")]
public class StudentController(IStudentService studentService) : ControllerBase
{
    private readonly IStudentService _studentService = studentService;
    
    [HttpGet]
    [Route("details/{id}")]
    [ProducesResponseType<StudentComprehensiveDetailsDto>(200)]
    public async Task<IActionResult> GetStudentDetailsById(Guid id)
    {
        var result = await _studentService.GetStudentDetailsById(id);
        return Ok(result);
    }
}