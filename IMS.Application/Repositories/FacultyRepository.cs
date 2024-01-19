using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;

namespace IMS.Application.Repositories;

public interface IFacultyRepositories
{
    Task<FacultyDto> CreateFaculty(FacultyDto faculty);
}

public class FacultyRepository(ImsContext context) : IFacultyRepositories
{
    private readonly ImsContext _context = context;

    public async Task<FacultyDto> CreateFaculty(FacultyDto faculty)
    {
        var toSave = faculty.Adapt<TblFaculty>();
        var saved =await _context.TblFaculties.AddAsync(toSave);
        return saved.Entity.Adapt<FacultyDto>();
    }
}