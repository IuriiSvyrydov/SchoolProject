using SchoolProject.Data.Entities.Procedures;
using StoredProcedureEFCore;

namespace SchoolProject.Infrastructure.Repositories.ProcedureRepo;

public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
{
    private readonly AppDbContext _appDbContext;

    public DepartmentStudentCountProcRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcAsync(DepartmentStudentCountProcParameters parameters)
    {
        var rows = new List<DepartmentStudentCountProc>();
        await _appDbContext.LoadStoredProc(nameof(DepartmentStudentCountProc))
              .AddParam(nameof(DepartmentStudentCountProcParameters.DID), parameters.DID)
              .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProc>());
        return rows;
    }
}