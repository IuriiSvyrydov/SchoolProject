using SchoolProject.Data.Entities;
using SchoolProject.Data.Entities.Procedures;
using SchoolProject.Data.Entities.Views;

namespace SchoolProject.Services.Abstract;

public interface IDepartmentService
{
    Task<Department> GetDepartmentByIdAsync(int departmentId);
    Task<bool> IsDepartmentIdExist(int? departmentId);
    Task<List<ViewDepartment>> GetDepartmentDataAsync();

    Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcAsync(
        DepartmentStudentCountProcParameters parameters);
}
