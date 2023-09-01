


namespace SchoolProject.Infrastructure.Repositories.DepartmentRepo
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private DbSet<Department> _departments;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _departments = context.Set<Department>();
        }
    }
}
