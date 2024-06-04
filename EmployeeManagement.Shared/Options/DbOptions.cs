namespace EmployeeManagement.Shared.Options;

public class DbOptions
{
    public static string SECTION = "DbOptions";
    public string ConnectionString { get; set; } = null!;
    public bool EnableSensitiveDataLogging { get; set; } = false;
    public bool EnableDetailedErrors { get; set; } = false;
}
