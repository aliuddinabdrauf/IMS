using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;

namespace IMS.Application.Repositories;

public interface IFacultyRepository
{
    Task<FacultyDto> CreateFaculty(FacultyDto faculty);
}

public class FacultyRepository(ImsContext context) : IFacultyRepository
{
    private readonly ImsContext _context = context;

    public async Task<FacultyDto> CreateFaculty(FacultyDto faculty)
    {
        var toSave = faculty.Adapt<TblFaculty>();
        var saved =await _context.TblFaculties.AddAsync(toSave);
        return saved.Entity.Adapt<FacultyDto>();
    }
}