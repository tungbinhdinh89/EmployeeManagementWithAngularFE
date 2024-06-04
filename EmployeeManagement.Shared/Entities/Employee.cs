using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Shared.Entities;

public class Employee
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required, MaxLength(256)]
    public string Name { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public DateTime Dob { get; set; }

    public string? Address { get; set; }
}