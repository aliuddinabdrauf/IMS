using IMS.Application.UoW;
using IMS.Infrastructure.Dto;

namespace IMS.Application.Services;

public interface IFacultyService
{
    Task<FacultyDto> CreateFaculty(FacultyDto facultyDto);
}

public class FacultyService(IUnitOfWork unitOfWork) : IFacultyService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<FacultyDto> CreateFaculty(FacultyDto facultyDto)
    {
        facultyDto.Id = null;
        var result = await _unitOfWork.FacultyRepository.CreateFaculty(facultyDto);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}