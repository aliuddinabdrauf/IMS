namespace IMS.Infrastructure.Dto;

public record ResponseDto(object? Data = null, string? Message = null, int Status = 200);
public class ResponseProblemDto{
 public string? Type {get; set;}
 public string? Title {get ;set;}
 public string? Details {get; set;}
 public int Status {get; set;}
}