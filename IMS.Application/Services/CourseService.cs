using IMS.Application.UoW;
using IMS.Infrastructure.Dto;

namespace IMS.Application.Services;

public interface ICourseServices
{
    Task<CourseDto> CreateCourse(CourseDto course);
    Task<List<CourseComprehensiveInfoDto>> GetAllCourseDetails();
}

public class CourseService(IUnitOfWork unitOfWork) : ICourseServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CourseDto> CreateCourse(CourseDto course)
    {
        course.Id = null;
        var result = await _unitOfWork.CourseRepositories.CreateCourse(course);
        await _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<List<CourseComprehensiveInfoDto>> GetAllCourseDetails()
    {
        var result = await _unitOfWork.CourseRepositories.GetAllCourseDetails();
        return result;
    }
}