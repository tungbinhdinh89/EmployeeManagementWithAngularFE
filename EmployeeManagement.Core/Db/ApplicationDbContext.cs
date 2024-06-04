using EmployeeManagement.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Core.Db;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; } = null!;



}
