using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using Mapster;

namespace IMS.Application.Repositories;

public interface IUserRepositories
{
    Task<UserDto> CreateUser(UserDto user);
    Task<List<UserDto>> GetUserNotSendEmailVerification();
}

public class UserRepository(ImsContext context) : IUserRepositories
{
    private readonly ImsContext _context = context;

    public async Task<UserDto> CreateUser(UserDto user)
    {
        var toSave = user.Adapt<TblUser>();
        var result = await _context.TblUsers.AddAsync(toSave);
        return result.Entity.Adapt<UserDto>();
    }

    public async Task<List<UserDto>> GetUserNotSendEmailVerification()
    {
        throw new NotImplementedException();
    }
}