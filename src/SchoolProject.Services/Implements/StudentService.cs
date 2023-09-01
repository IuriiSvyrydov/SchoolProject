
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Enums;


namespace SchoolProject.Services.Implements;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;
    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<string> AddAsync(Student student)
    {
        var studentResult = _studentRepository.GetTableNoTracking()
                            .Where(x => x.NameUa.Equals(student.NameUa)
                             && x.NameUs.Equals(student.NameUs))
                             .FirstOrDefault();
        if (studentResult != null) return "Exist";
        _studentRepository.AddAsync(student);
        return "Success";
    }
    public async Task<string> DeleteAsync(Student student)
    {
        var transaction = _studentRepository.BeginTransaction();
        try
        {
            student.Address = "Kharkiv";
            await _studentRepository.UpdateAsync(student);
            await _studentRepository.DeleteAsync(student);
            transaction.Commit();
            return "Success";

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return "Failed";
        }
    }
    public async Task<string> EditAsync(Student student)
    {
        await _studentRepository.UpdateAsync(student);
        return "Success";
    }

    public IQueryable<Student> FilterStudentPaginatedQuerable(SudentOrderEnum order, string search)
    {
        var query = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
        if (search != null)
        {
            query = query.Where(x => x.NameUa.Contains(search) || x.Address.Contains(search));
        }
        switch (order)
        {
            case SudentOrderEnum.StudId:
                query = query.OrderBy(x => x.StudID);
                break;
            case SudentOrderEnum.Name:
                query = query.OrderBy(x => x.NameUa);
                break;
            case SudentOrderEnum.Address:
                query = query.OrderBy(x => x.Address);
                break;
            case SudentOrderEnum.DepartmentName:
                query = query.OrderBy(x => x.Department.NameUa);
                break;
        }

        return query;
    }

    public async Task<Student> GetStudentByIdAsync(int studentId)
    {
        var student = await _studentRepository.GetByIdAsync(studentId);
        return student;
    }
    public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
    {
        var student = _studentRepository.GetTableNoTracking()
              .Include(x => x.Department)
              .Where(x => x.StudID.Equals(id))
              .FirstOrDefault();
        return student;
    }
    public async Task<List<Student>> GetStudentListAsync()
    {
        return await _studentRepository.GetStudentListAsync();
    }

    public IQueryable<Student> GetStudentsByDepartmentIdQuerable(int id)
    {
        return _studentRepository.GetTableNoTracking().Where(x => x.DID.Equals(id)).AsQueryable();
    }




    public IQueryable<Student> GetStudentsQuerable()
    {
        return _studentRepository.GetTableNoTracking()
              .Include(x => x.Department).AsQueryable();
    }
    public async Task<bool> IsNameExist(string name)
    {
        var student = _studentRepository.GetTableAsNoTracking()
            .Where(x => x.NameUa.Equals(name) && x.NameUs.Equals(name))
            .FirstOrDefault();
        if (student is null)
            return false;
        return true;
    }
    public async Task<bool> IsNameExistExcludeSelf(string name, int id)
    {
        var student = await _studentRepository.GetTableAsNoTracking()
            .Where(x => x.NameUa.Equals(name) && x.NameUs.Equals(name) & !x.StudID.Equals(id))
            .FirstOrDefaultAsync();
        if (student is null)
            return false;
        return true;
    }
}

