using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace IMS.Application.Repositories;

public interface ICourseRepository
{
    Task<CourseDto> CreateCourse(CourseDto course);
    Task<List<CourseComprehensiveInfoDto>> GetAllCourseDetails();
}

public class CourseRepository(ImsContext context) : ICourseRepository
{
    private readonly ImsContext _context = context;

    public async Task<CourseDto> CreateCourse(CourseDto course)
    {
        var toSave = course.Adapt<TblCourse>();
        var saved = await _context.TblCourses.AddAsync(toSave);
        return saved.Entity.Adapt<CourseDto>();
    }

    public async Task<List<CourseComprehensiveInfoDto>> GetAllCourseDetails()
    {
        var data = await _context.TblCourses.Include(c => c.Faculty)
            .Select(o => o.Adapt<CourseComprehensiveInfoDto>()).ToListAsync();
        return data;
    }
}