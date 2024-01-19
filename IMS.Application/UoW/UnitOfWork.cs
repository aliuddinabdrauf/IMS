using IMS.Application.Repositories;
using IMS.Infrastructure;
using IMS.Infrastructure.DbContext.IMS;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace IMS.Application.UoW;

public interface IUnitOfWork
{
    IStudentRepository StudentRepository { get; }
    IUserRepository UserRepository { get; }
    IFacultyRepository FacultyRepository { get; }
    ICourseRepository CourseRepository { get; }
    IAuthenticationRepository AuthenticationRepository { get; }
    IEmailRepository EmailRepository { get; }
    int Complete();
    Task<int> CompleteAsync();
    void Dispose();
    Task DisposeAsync();
}

public class UnitOfWork(ImsContext context, IStringLocalizer<GlobalResource> globalResource, IMemoryCache memoryCache) : IUnitOfWork
{
    #region private
    private readonly ImsContext _context = context;
    #region initialize lazy Repository
    //use lazy since we dont want any Repository that not being used is initialized
    private readonly Lazy<IStudentRepository> _studentRepository = new (()=>new StudentRepository(context, globalResource));
    private readonly Lazy<IUserRepository> _userRepository = new (()=>new UserRepository(context, globalResource,memoryCache));
    private readonly Lazy<IFacultyRepository> _facultyRepository = new(() => new FacultyRepository(context));
    private readonly Lazy<ICourseRepository> _courseRepository = new(() => new CourseRepository(context));
    private readonly Lazy<IAuthenticationRepository> _authenticationRepository = new(() => new AuthenticationRepository(context, globalResource));
    private readonly Lazy<IEmailRepository> _emailRepository = new(() => new EmailRepository(context));
    #endregion
    #endregion
    
    public IStudentRepository StudentRepository => _studentRepository.Value;
    public IUserRepository UserRepository => _userRepository.Value;
    public IFacultyRepository FacultyRepository => _facultyRepository.Value;
    public ICourseRepository CourseRepository => _courseRepository.Value;
    public IAuthenticationRepository AuthenticationRepository => _authenticationRepository.Value;
    public IEmailRepository EmailRepository => _emailRepository.Value;
    

    public int Complete()
    {
        return _context.SaveChanges();
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
    public async Task DisposeAsync()
    {
         await _context.DisposeAsync();
    }
}