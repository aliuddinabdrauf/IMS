using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace IMS.Application.Repositories;

public interface IStudentRepositories
{
    Task<StudentDto> CreateStudent(StudentDto student);
    Task MapStudentToCourses(Guid studentId, Guid[] courseIds);
    Task<StudentComprehensiveDetailsDto> GetStudentDetailsById(Guid id);
}

public class StudentRepository(ImsContext context, IStringLocalizer<GlobalResource> globalResource) : IStudentRepositories
{
    private readonly ImsContext _context = context;
    private readonly IStringLocalizer<GlobalResource> _stringLocalizer = globalResource;

    public async Task<StudentDto> CreateStudent(StudentDto student)
    {
       var result = await _context.TblStudents.AddAsync(student.Adapt<TblStudent>());
       return result.Entity.Adapt<StudentDto>();
    }

    public async Task MapStudentToCourses(Guid studentId, Guid[] courseIds)
    {
        List<TblStudentCourse> newStudentCourses = [];
        foreach (var courseId in courseIds)
        {
            newStudentCourses.Add(new TblStudentCourse()
            {
                StudentId = studentId,
                CourseId = courseId
            });
        }
        await _context.TblStudentCourses.AddRangeAsync(newStudentCourses);
    }

    public async Task<StudentComprehensiveDetailsDto> GetStudentDetailsById(Guid id)
    {
        var query = await _context.TblStudents.Include(s => s.StudentCourses)
            .ThenInclude(sc => sc.Course)
            .Select(student => new {student, courses = student.StudentCourses.Select(o => o.Course)})
            .SingleOrDefaultAsync(s => s.student.Id == id);
        if (query == null) throw new RecordNotFoundException(_stringLocalizer["RecordNotFound"]);
        var result = query.student.Adapt<StudentComprehensiveDetailsDto>(); 
        result.Courses = query.courses.Adapt<List<CourseDto>>();
        return result;

    }
}