

namespace SchoolProject.Infrastructure.Configuration;
public class StudentSubjectConfiguration : IEntityTypeConfiguration<StudentSubject>
{
    public void Configure(EntityTypeBuilder<StudentSubject> builder)

    {
        builder.HasKey(x => new
        {
            x.StudID,
            x.SubID
        });

            builder.HasOne(ds => ds.Student)
                .WithMany(d => d.StudentSubject)
                .HasForeignKey(ds => ds.StudID);
           builder.HasOne(ds => ds.Subject)
                .WithMany(ds => ds.StudentsSubjects)
               .HasForeignKey(ds => ds.SubID);

    }
}
