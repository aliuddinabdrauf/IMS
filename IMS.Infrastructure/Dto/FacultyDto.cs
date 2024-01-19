using System.ComponentModel.DataAnnotations;

namespace IMS.Infrastructure.Dto;

public record FacultyDto(
    Guid? Id,
    [param: Required]
    [param: StringLength(maximumLength: 10)]
    string Code,
    [param: Required]
    [param: StringLength(maximumLength: 200)]
    string Name,
    [param: StringLength(maximumLength: 1000)]
    string? Description)
{
    public Guid? Id { get; set; } = Id;
}