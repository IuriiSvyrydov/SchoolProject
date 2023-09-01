namespace SchoolProject.Infrastructure.Repositories.SubjectRepo;

public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
{
    private readonly DbSet<Subject> _subjects;

    public SubjectRepository(AppDbContext context) : base(context)
    {
        _subjects = context.Set<Subject>();
    }
}
