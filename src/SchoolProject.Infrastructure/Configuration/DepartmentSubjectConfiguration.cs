
namespace SchoolProject.Infrastructure.Configuration;

public class DepartmentSubjectConfiguration : IEntityTypeConfiguration<DepartmentSubject>
{
    public void Configure(EntityTypeBuilder<DepartmentSubject> builder)
    {
        builder
            .HasKey(x => new { x.SubId, x.DID });
        builder.HasOne(ds => ds.Department)
            .WithMany(ds => ds.DepartmentSubjects)
            .HasForeignKey(ds => ds.DID);
        builder.HasOne(ds => ds.Subject)
            .WithMany(d => d.DepartmetsSubjects)
            .HasForeignKey(ds => ds.SubId);

    }
}

