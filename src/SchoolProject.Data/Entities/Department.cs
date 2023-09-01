using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities;

[Table("Departments", Schema = "dbo.")]
public class Department : GeneralLocalizableEntity
{
    public Department()
    {
        Students = new HashSet<Student>();
        DepartmentSubjects = new HashSet<DepartmentSubject>();
        //  Instructors = new HashSet<Instructor>();
    }

    [Key]
    public int DID { get; set; }

    //[StringLength(500)]
    public string? NameUs { get; set; }

    [StringLength(200)]
    public string? NameUa { get; set; }

    public int? InsManager { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Student> Students { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<DepartmentSubject> DepartmentSubjects { get; set; }

    [InverseProperty("department")]
    public virtual ICollection<Instructor> Instructors { get; set; }

    [ForeignKey("InsManager")]
    [InverseProperty("departmentManager")]
    public virtual Instructor? Instructor { get; set; }
}