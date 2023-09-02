using EntityFrameworkCore.EncryptColumn.Extension;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>,
 IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    private readonly IEncryptionProvider _encryptionProvider;
    public AppDbContext(IEncryptionProvider encryptionProvider)
    {

    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        _encryptionProvider = new GenerateEncryptionProvider("73827F235D5546DD979760F4806A905C");
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<DepartmentSubject> DepartmentSubjects { get; set; }
    public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    //Views
    public DbSet<ViewDepartment> ViewDepartment { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new DepartmentSubjectConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorConfiguration());
        modelBuilder.ApplyConfiguration(new InstructorSubjectConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentSubjectConfiguration());
        //  modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.UseEncryption(_encryptionProvider);

        base.OnModelCreating(modelBuilder);
    }
}

