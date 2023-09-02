using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Infrastructure.Repositories.ViewRepo;

public class ViewDepartmentRepository : GenericRepositoryAsync<ViewDepartment>, IViewRepository<ViewDepartment>
{
    private readonly DbSet<ViewDepartment> _viewDepartments;
    public ViewDepartmentRepository(AppDbContext context) : base(context)
    {
        _viewDepartments = context.Set<ViewDepartment>();
    }
}