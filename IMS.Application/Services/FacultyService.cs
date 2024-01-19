using IMS.Application.UoW;
using IMS.Infrastructure.Dto;

namespace IMS.Application.Services;

public interface IFacultyServices
{
    Task<FacultyDto> CreateFaculty(FacultyDto facultyDto);
}

public class FacultyService(IUnitOfWork unitOfWork) : IFacultyServices
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<FacultyDto> CreateFaculty(FacultyDto facultyDto)
    {
        facultyDto.Id = null;
        var result = await _unitOfWork.FacultyRepositories.CreateFaculty(facultyDto);
        await _unitOfWork.CompleteAsync();
        return result;
    }
}