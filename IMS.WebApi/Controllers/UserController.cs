using IMS.Application.Services;
using IMS.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;

[ApiController]
[Route("api/user")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    [HttpPut]
    [Route("create/student")]
    [ProducesResponseType<StudentDto>(200)]
    public async Task<IActionResult> CreateNewStudent(CreateUserAsStudentDto student)
    {
        var result = await _userService.CreateUserAsStudent(student);
        return Ok(result);
    }
}