namespace SchoolProject.Infrastructure.Repositories.InstructorRepo;

public class InsructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
{
    private readonly DbSet<Instructor> _instructors;

    public InsructorRepository(AppDbContext context) : base(context)
    {
        _instructors = context.Set<Instructor>();
    }
}
