using EmployeeManagement.Core.Db;
using EmployeeManagement.Shared.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EmployeeManagement.Server;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] agrs)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var dbOptions = configuration.GetSection(DbOptions.SECTION).Get<DbOptions>() ?? throw new Exception("DbOptions not found");
        Console.WriteLine($"connectionstring: {dbOptions.ConnectionString}");

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(dbOptions.ConnectionString)
            .Options;

        return new ApplicationDbContext(dbContextOptions);
    }
}
