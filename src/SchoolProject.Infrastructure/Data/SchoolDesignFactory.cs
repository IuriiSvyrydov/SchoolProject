
namespace SchoolProject.Infrastructure.Data;

public class SchoolDesignFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer("Server=REVISION-PC;Database=SchoolDb;Trusted_Connection=True;Integrated Security=True;TrustServerCertificate=true;");

        return new AppDbContext(optionsBuilder.Options);
    }
}

