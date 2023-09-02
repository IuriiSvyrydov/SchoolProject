

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;
using SchoolProject.Infrastructure.Repositories.ProcedureRepo;
using SchoolProject.Infrastructure.Repositories.ViewRepo;

namespace SchoolProject.Services.Implements;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IViewRepository<ViewDepartment> _viewRepository;
    private readonly IDepartmentStudentCountProcRepository _departmentStudentCountProcRepository;
    public DepartmentService(IDepartmentRepository departmentRepository, IViewRepository<ViewDepartment> viewRepository,
        IDepartmentStudentCountProcRepository departmentStudentCountProcRepository)
    {
        _departmentRepository = departmentRepository;
        _viewRepository = viewRepository;
        _departmentStudentCountProcRepository = departmentStudentCountProcRepository;
    }
    public async Task<Department> GetDepartmentByIdAsync(int departmentId)
    {
        var department = await _departmentRepository.GetTableNoTracking().Where(x => x.DID.Equals(departmentId))
              .Include(x => x.DepartmentSubjects).Include(x => x.Students)
              .Include(x => x.Instructors)
              .Include(x => x.Instructor).FirstOrDefaultAsync();
        return department;

    }

    public async Task<bool> IsDepartmentIdExist(int? departmentId)
    {
        return await _departmentRepository.GetTableNoTracking().AnyAsync(x => x.DID.Equals(departmentId));
    }

    public async Task<List<ViewDepartment>> GetDepartmentDataAsync()
    {
        var viewDepartment = await _viewRepository.GetTableNoTracking().ToListAsync();
        return viewDepartment;
    }

    public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcAsync(DepartmentStudentCountProcParameters parameters)
    {
        var procDepartment = await _departmentStudentCountProcRepository.GetDepartmentStudentCountProcAsync(parameters);
        return procDepartment;
    }
}
