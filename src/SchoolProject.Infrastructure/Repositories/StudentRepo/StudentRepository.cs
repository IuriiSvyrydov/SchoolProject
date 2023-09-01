

namespace SchoolProject.Infrastructure.Repositories.StudentRepo;

public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
{
    private readonly DbSet<Student> _students;

    public StudentRepository(AppDbContext context) : base(context)
    {
        _students = context.Set<Student>();
    }
    public async Task<List<Student>> GetStudentListAsync()
        => await _students.Include(x => x.Department).ToListAsync();
}

