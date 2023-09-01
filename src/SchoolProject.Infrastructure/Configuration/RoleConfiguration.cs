using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Configuration;


public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(new Role() { Name = "Admin" },
                        new Role() { Name = "User" });
    }
}
