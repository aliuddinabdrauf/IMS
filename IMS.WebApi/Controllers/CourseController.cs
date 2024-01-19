using IMS.Application.Attributes;
using IMS.Application.Services;
using IMS.Infrastructure.Dto;
using Microsoft.AspNetCore.Mvc;

namespace IMS.WebApi.Controllers;
[ApiController]
[Route("api/course")]
public class CourseController(ICourseService courseService) : ControllerBase
{
    private readonly ICourseService _courseService = courseService;
    [Route("getall")]
    [ProducesResponseType<List<CourseComprehensiveInfoDto>>(200)]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllCourseDetails()
    {
       var result = await _courseService.GetAllCourseDetails();
       return Ok(result);
    }
    
    [HttpPut]
    [Route("create")]
    [ProducesResponseType<CourseDto>(200)]
    public async Task<IActionResult> CreateNewCourse(CourseDto course)
    {
        var result = await _courseService.CreateCourse(course);
        return Ok(result);
    }
}