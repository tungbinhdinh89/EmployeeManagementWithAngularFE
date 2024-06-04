namespace EmployeeManagement.Shared.Entities;

public class Employee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Gender { get; set; }
    public string? Phone { get; set; }
    public DateTime Dob { get; set; }
    public string? Address { get; set; }
}