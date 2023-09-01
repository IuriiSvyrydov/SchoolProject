using SchoolProject.Data.Entities;

namespace SchoolProject.Services.Abstract;

public interface IDepartmentService
{
    Task<Department> GetDepartmentByIdAsync(int departmentId);
    Task<bool> IsDepartmentIdExist(int? departmentId);
}
