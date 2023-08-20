using Microsoft.EntityFrameworkCore;
namespace IMSInfrastructure.DbContext.IMS;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

public class ImsContext : DbContext
{
    public ImsContext()
    {
    }

    public ImsContext(DbContextOptions<ImsContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<TblUser> TblUser { get; set; }
    public virtual DbSet<TblStudent> TblStudent { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ims;User Id=ims_user;Password=123456789;", o => o.UseNodaTime());
        }
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum<UserRole>()
            .HasPostgresEnum<UserStatus>()
            .HasPostgresEnum<UserType>()
            .HasPostgresEnum<UserGender>();
    }
}