using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities;

[Table("Students", Schema = "dbo.")]
public class Student : GeneralLocalizableEntity
{
    public Student()
    {
        StudentSubject = new HashSet<StudentSubject>();
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudID { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    public string NameUa { get; set; }
    public string NameUs { get; set; }

    [StringLength(500)]
    public string? Phone { get; set; }

    public int? DID { get; set; }

    [ForeignKey("DID")]
    [InverseProperty("Students")]
    public virtual Department? Department { get; set; }

    [InverseProperty("Student")]
    public virtual ICollection<StudentSubject> StudentSubject { get; set; }
}