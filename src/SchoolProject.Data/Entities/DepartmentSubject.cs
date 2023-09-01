using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities;

[Table("DepartmentSubjects", Schema = "dbo.")]
public class DepartmentSubject : GeneralLocalizableEntity
{
    [Key]
    public int DeptSubjectId { get; set; }

    public int DID { get; set; }
    public int SubId { get; set; }

    [ForeignKey("DID")]
    [InverseProperty("DepartmentSubjects")]
    public virtual Department? Department { get; protected set; }

    [ForeignKey("SubId")]
    public virtual Subject? Subject { get; protected set; }
}