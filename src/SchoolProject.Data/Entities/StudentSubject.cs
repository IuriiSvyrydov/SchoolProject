namespace SchoolProject.Data.Entities;

[Table("StudentSubjects", Schema = "dbo.")]
public class StudentSubject
{
    [Key]
    public int StudSubId { get; set; }

    public int StudID { get; set; }
    public int SubID { get; set; }
    public decimal? Grade { get; set; }

    [ForeignKey("StudID")]
    [InverseProperty("StudentSubject")]
    public virtual Student? Student { get; set; }

    [ForeignKey("SubID")]
    [InverseProperty("StudentsSubjects")]
    public virtual Subject? Subject { get; set; }
}