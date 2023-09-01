
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;

namespace SchoolProject.Services.Abstract;

public interface IStudentService
{
    Task<List<Student>> GetStudentListAsync();
    Task<Student> GetStudentByIdAsync(int studentId);
    Task<Student> GetStudentByIdWithIncludeAsync(int id);
    Task<string> AddAsync(Student student);
    Task<bool> IsNameExist(string name);
    Task<bool> IsNameExistExcludeSelf(string name, int id);
    Task<string> EditAsync(Student student);
    Task<string> DeleteAsync(Student student);
    IQueryable<Student> GetStudentsQuerable();
    IQueryable<Student> GetStudentsByDepartmentIdQuerable(int id);
    IQueryable<Student> FilterStudentPaginatedQuerable(SudentOrderEnum order, string search);
}

