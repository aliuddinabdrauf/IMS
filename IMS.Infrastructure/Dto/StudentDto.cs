using IMS.Infrastructure.DbContext.IMS;

namespace IMS.Infrastructure.Dto;

public record StudentDto(
    Guid? Id,
    string Name,
    string IcNo,
    string StudentId,
    List<string> PhoneNo,
    List<string> Address,
    UserGender Gender,
    string[] CourseCodes,
    Guid UserId
)
{
    public required Guid UserId { get; set; } = UserId;
};

//TODO: add course property here
public record StudentComprehensiveDetailsDto(
    Guid Id,
    string Name,
    string IcNo,
    List<string> PhoneNo,
    List<string> Address,
    UserGender Gender,
    List<CourseDto> Courses
)
{
    public List<CourseDto> Courses { get; set; } = Courses;
}