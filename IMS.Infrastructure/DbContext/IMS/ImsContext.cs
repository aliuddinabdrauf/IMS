using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IMS.Infrastructure.DbContext.IMS;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

public partial class ImsContext : DbContext
{
    public ImsContext()
    {
    }

    public ImsContext(DbContextOptions<ImsContext> options)
        : base(options)
    {
    }
    public virtual DbSet<TblBase> TblBases { get; set; }
    public virtual DbSet<TblUser> TblUsers { get; set; }
    public virtual DbSet<TblStudent> TblStudents { get; set; }
    public virtual DbSet<TblStaff> TblStaffs { get; set; }
    public virtual DbSet<TblFaculty> TblFaculties { get; set; }
    public virtual DbSet<TblCourse> TblCourses { get; set; }
    public virtual DbSet<TblStudentCourse> TblStudentCourses { get; set; }
    public virtual DbSet<TblResetPassword> TblResetPasswords { get; set; }
    public virtual DbSet<TblEmail> TblEmails { get; set; }
    public virtual DbSet<TblLoginSession> TblLoginSessions { get; set; }
    public virtual DbSet<TblFile> TblFiles {get; set;}
    public virtual DbSet<TblFileDetail> TblFileDetails {get ;set;}

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
        //register enum here, will be available in type, in postgres db
        modelBuilder
            .HasPostgresEnum<UserRole>()
            .HasPostgresEnum<UserStatus>()
            .HasPostgresEnum<AccountType>()
            .HasPostgresEnum<UserGender>()
            .HasPostgresEnum<FileType>();
        
        //model entity configuration
        modelBuilder.Entity<TblBase>(entity =>
        {
            entity.ToTable("tbl_base");
            entity.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("uuid_generate_v1()");
            entity.Property(e => e.TimestampCreated).ValueGeneratedOnAdd();
            //entity.Property(e => e.TimestampUpdated).ValueGeneratedOnAddOrUpdate();
        });
        modelBuilder.Entity<TblBase>().UseTpcMappingStrategy();
        
        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.ToTable("tbl_user");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(100);

            entity.Property(e => e.PasswordHash)
                .HasMaxLength(200);

            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(100);

            entity.Property(e => e.Roles)
                .HasColumnType("user_role[]");
            entity.Property(e => e.Type)
                .HasColumnType("account_type");
            entity.Property(e => e.Status)
                .HasColumnType("user_status");

            entity.HasOne(d => d.ProfilePitcureFile).WithOne(p => p.User)
                .HasForeignKey<TblUser>(d =>d.ProfilePicture)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasIndex(e => new { e.EmailAddress }).IsUnique();
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.ToTable("tbl_student");

            entity.Property(e => e.Name)
                .HasMaxLength(200);
            
            entity.Property(e => e.StudentId)
                .HasMaxLength(10)
                .IsFixedLength();
            
            entity.Property(e => e.IcNo)
                .HasMaxLength(12)
                .IsFixedLength();
            
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(12);
            
            entity.Property(e => e.Address)
                .HasMaxLength(300);
            
            entity.Property(e => e.Gender)
                .HasColumnType("user_gender");
            
            entity.HasIndex(e => new { e.StudentId, e.IcNo }).IsUnique();
            
            entity.HasOne(d => d.User).WithOne(p => p.Student)
                .HasForeignKey<TblStudent>(d =>d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<TblStaff>(entity =>
        {
            entity.ToTable("tbl_staff");

            entity.Property(e => e.Name)
                .HasMaxLength(200);
            
            entity.Property(e => e.StaffId)
                .HasMaxLength(10)
                .IsFixedLength();
            
            entity.Property(e => e.Gender)
                .HasColumnType("user_gender");
            
            entity.HasIndex(e => new { e.StaffId }).IsUnique();
            
            entity.HasOne(d => d.User).WithOne(p => p.Staff)
                .HasForeignKey<TblStaff>(d =>d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<TblFaculty>(entity =>
        {
            entity.ToTable("tbl_faculty");

            entity.Property(e => e.Code)
                .HasMaxLength(10);
            
            entity.Property(e => e.Name)
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.HasIndex(e => e.Code).IsUnique();
        });
        
        modelBuilder.Entity<TblCourse>(entity =>
        {
            entity.ToTable("tbl_course");

            entity.Property(e => e.Code)
                .HasMaxLength(10);
            
            entity.Property(e => e.Name)
                .HasMaxLength(200);
            
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
            
            entity.HasOne(d => d.Faculty).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasIndex(e => e.Code).IsUnique();
        });

        modelBuilder.Entity<TblStudentCourse>(entity =>
        {
            entity.ToTable("tbl_student_course");
            
            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
        });

        modelBuilder.Entity<TblResetPassword>(entity =>
        {
            entity.ToTable("tbl_reset_password");
            
            entity.Property(e => e.ResetKey)
                .HasMaxLength(100);
            
            entity.HasOne(d => d.User).WithMany(p => p.ResetPasswords)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            
        });

        modelBuilder.Entity<TblLoginSession>(entity =>
        {
            entity.ToTable("tbl_login_session");
            entity.HasOne(d => d.User).WithMany(p => p.LoginSessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<TblEmail>(entity =>
        {
            entity.ToTable("tbl_email");
            entity.Property(e => e.Subject)
                .HasMaxLength(100);
            entity.Property(e => e.Reference)
                .HasMaxLength(30);
            entity.Property(e => e.Sender)
                .HasMaxLength(100);
            entity.HasIndex(e => new { e.ReferenceId });
        });

        modelBuilder.Entity<TblFile>(entity =>
        {
            entity.ToTable("tbl_file");
            entity.Property(e => e.File).HasMaxLength(1000000000);
        });

        modelBuilder.Entity<TblFileDetail>(entity =>
        {
            entity.ToTable("tbl_file_detail");
            entity.Property(e => e.Type)
                .HasColumnType("file_type");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.HasOne(d => d.FileActual).WithMany(p => p.Actuals)
                .HasForeignKey(d => d.Actual)
                .OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(d => d.FilePreview).WithMany(p => p.Previews)
                .HasForeignKey(d => d.Preview)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}