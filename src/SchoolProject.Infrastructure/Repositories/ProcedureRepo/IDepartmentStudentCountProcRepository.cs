using SchoolProject.Data.Entities.Procedures;

namespace SchoolProject.Infrastructure.Repositories.ProcedureRepo;

public interface IDepartmentStudentCountProcRepository
{
    Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcAsync(DepartmentStudentCountProcParameters departmentStudentCountProcParameters);
}