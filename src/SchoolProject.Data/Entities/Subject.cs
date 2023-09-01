using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities;

[Table("Subjects", Schema = "dbo.")]
public class Subject : GeneralLocalizableEntity
{
    public Subject()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmetsSubjects = new HashSet<DepartmentSubject>();
        Ins_Subjects = new HashSet<Ins_Subject>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubID { get; set; }

    [StringLength(500)]
    public string? SubjectNameUs { get; set; }

    public string? SubjectNameUa { get; set; }
    public int? Period { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<DepartmentSubject> DepartmetsSubjects { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
}