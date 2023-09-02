using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities.Procedures;

public class DepartmentStudentCountProc : GeneralLocalizableEntity
{
    public int DID { get; set; }
    public string? NameUa { get; set; }
    public string? NameUs { get; set; }
    public int StudentCount { get; set; }
}

public class DepartmentStudentCountProcParameters
{
    public int DID { get; set; } = 0;
}