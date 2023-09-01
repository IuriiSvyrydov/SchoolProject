namespace SchoolProject.Infrastructure.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.StudID);
        builder.HasOne(x => x.Department)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.DID)
             .OnDelete(DeleteBehavior.Restrict);

    }
}
