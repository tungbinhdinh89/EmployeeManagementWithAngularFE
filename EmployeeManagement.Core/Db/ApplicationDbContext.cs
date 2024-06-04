using EmployeeManagement.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Core.Db;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

        modelBuilder.Entity<Employee>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
    }

}
