using System.Diagnostics;

namespace EmployeeManagement.Core.Common;

[DebuggerStepThrough]
[DebuggerDisplay("Current:{Current}, PerPage:{PerPage}")]
public record PagingRequest
{
    public int Current { get; set; } = 0;
    public int PerPage { get; set; } = 100;
    public string? Search { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public string? SortDir { get; set; } = null;
    public bool IsAsc => string.Equals(SortDir, "asc", StringComparison.OrdinalIgnoreCase);
}