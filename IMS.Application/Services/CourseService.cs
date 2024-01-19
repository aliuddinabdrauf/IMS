using IMS.Application.UoW;
using IMS.Infrastructure.Dto;

namespace IMS.Application.Services;

public interface ICourseService
{
    Task<CourseDto> CreateCourse(CourseDto course);
    Task<List<CourseComprehensiveInfoDto>> GetAllCourseDetails();
}

public class CourseService(IUnitOfWork unitOfWork) : ICourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CourseDto> CreateCourse(CourseDto course)
    {
        course.Id = null;
        var result = await _unitOfWork.CourseRepository.CreateCourse(course);
        await _unitOfWork.CompleteAsync();
        return result;
    }

    public async Task<List<CourseComprehensiveInfoDto>> GetAllCourseDetails()
    {
        var result = await _unitOfWork.CourseRepository.GetAllCourseDetails();
        return result;
    }
}