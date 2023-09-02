using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Repositories.ProcedureRepo;
using SchoolProject.Infrastructure.Repositories.RefreshTokenRepo;
using SchoolProject.Infrastructure.Repositories.ViewRepo;

namespace SchoolProject.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataExtension(configuration);
        services.AddBaseRepository();

        return services;
    }

    public static IServiceCollection AddDataExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
                                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
    public static IServiceCollection AddBaseRepository(this IServiceCollection services)
    {
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IInstructorRepository, InsructorRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();

        //views
        services.AddScoped<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();
        return services;
    }


}





