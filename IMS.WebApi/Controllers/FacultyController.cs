using IMS.Application.Services;
using IMS.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;

[ApiController]
[Route("api/faculty")]
public class FacultyController(IFacultyService facultyService): ControllerBase
{
    private readonly IFacultyService _facultyService = facultyService;
    [HttpPut]
    [Route("create")]
    [ProducesResponseType<FacultyDto>(200)]
    public async Task<IActionResult> CreateNewFaculty(FacultyDto faculty)
    {
        var result = await _facultyService.CreateFaculty(faculty);
        return Ok(result);
    }

}