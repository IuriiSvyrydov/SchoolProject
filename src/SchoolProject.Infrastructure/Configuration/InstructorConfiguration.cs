namespace SchoolProject.Infrastructure.Configuration;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder
            .HasKey(x => x.InsId);
        builder
             .HasOne(x => x.Supervisor)
             .WithMany(x => x.Instructors)
             .HasForeignKey(x => x.SupervisorId)
             .OnDelete(DeleteBehavior.Restrict);
    }
}