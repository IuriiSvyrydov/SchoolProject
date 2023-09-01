

using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBase.Abstracts;

namespace SchoolProject.Services.Implements;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
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
        return await _departmentRepository.GetTableNoTracking().AnyAsync(x=>x.DID.Equals(departmentId));
    }
}
