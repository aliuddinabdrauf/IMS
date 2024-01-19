using System.ComponentModel.DataAnnotations;

namespace IMS.Infrastructure.Dto;

public record CourseDto(
    Guid? Id,
    [param: Required] Guid FacultyId,
    [param: Required]
    [param: StringLength(maximumLength: 10)]
    string Code,
    [param: Required]
    [param: StringLength(maximumLength: 200)]
    string Name,
    [param: StringLength(maximumLength: 1000)]
    string Description)
{
    public Guid FacultyId { get; set; } = FacultyId;
    public Guid? Id { get; set; } = Id;
}

public record CourseComprehensiveInfoDto(Guid Id, string Name, string Code, string Description, Guid FacultyId, string FacultyName);