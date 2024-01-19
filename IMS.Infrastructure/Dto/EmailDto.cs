namespace IMS.Infrastructure.Dto;

public record EmailDto(
    string Sender,
    List<string> To,
    List<string> Cc,
    List<string> Bcc,
    string? Subject,
    string Body,
    string? Reference,
    Guid? ReferenceId);