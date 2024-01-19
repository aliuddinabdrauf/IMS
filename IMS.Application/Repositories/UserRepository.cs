using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using IMS.Infrastructure.Dto;
using IMS.Infrastructure.Util;
using Mapster;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace IMS.Application.Repositories;

public interface IUserRepository
{
    Task<UserDto> CreateUser(UserDto user);
    Task<List<UserDto>> GetUserNotSendEmailVerification();
    Task<UserDto> GetUserByEmailAddress(string emailAddress);
    Task<UserDto> GetUserForLogin(LoginDto login);
    Task<UserDto> GetUserActiveById(Guid id);
    void UpdatePasswordHashAndSalt(ResetUserPasswordDto resetUserPasswordDto);
}

public class UserRepository(ImsContext context, IStringLocalizer<GlobalResource> stringLocalizer, IMemoryCache memoryCache) : IUserRepository
{
    private readonly ImsContext _context = context;
    private IStringLocalizer<GlobalResource> _stringLocalizer = stringLocalizer;
    private readonly IMemoryCache _memoryCache = memoryCache;

    public async Task<UserDto> GetUserByEmailAddress(string emailAddress)
    {
        var user = await _context.TblUsers.SingleOrDefaultAsync(o => o.EmailAddress == emailAddress.ToLowerInvariant());
        if (user == null) throw new RecordNotFoundException(_stringLocalizer["RecordNotFound"]);
        return user.Adapt<UserDto>();
    }
    
    public async Task<UserDto> GetUserForLogin(LoginDto login)
    {
        var user = await _context.TblUsers.SingleOrDefaultAsync(o => o.EmailAddress == login.EmailAddress.ToLowerInvariant() && o.Type == login.AccountType);
        if (user == null) throw new RecordNotFoundException(_stringLocalizer["RecordNotFound"]);
        return user.Adapt<UserDto>();
    }
    public async Task<UserDto> GetUserActiveById(Guid id)
    {
        if (!_memoryCache.TryGetValue(id, out UserDto? cache) || cache == null)
        {
            var user = await _context.TblUsers.SingleOrDefaultAsync(o => o.Id == id && o.Status == UserStatus.Active);
            if (user == null) throw new RecordNotFoundException(_stringLocalizer["RecordNotFound"]);
            var result = user.Adapt<UserDto>();
            _memoryCache.Set(id, result, TimeSpan.FromMinutes(10));
            return result;
        }
        return cache;
    }

    public async Task<UserDto> CreateUser(UserDto user)
    {
        var toSave = user.Adapt<TblUser>();
        var result = await _context.TblUsers.AddAsync(toSave);
        return result.Entity.Adapt<UserDto>();
    }

    public void UpdatePasswordHashAndSalt(ResetUserPasswordDto resetUserPasswordDto)
    {
        var toSave = resetUserPasswordDto.Adapt<TblUser>();
        var ent = context.TblUsers.Attach(toSave);
        toSave.Status = UserStatus.Active;
        ent.PrepareUpdate(nameof(toSave.PasswordHash),nameof(toSave.PasswordSalt));
    }

    public async Task<List<UserDto>> GetUserNotSendEmailVerification()
    {
        throw new NotImplementedException();
    }
}