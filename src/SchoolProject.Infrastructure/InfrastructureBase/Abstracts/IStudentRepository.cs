

namespace SchoolProject.Infrastructure.InfrastructureBase.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetStudentListAsync();
    }
}
