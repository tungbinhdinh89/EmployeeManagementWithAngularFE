using EmployeeManagement.Core.Common;
using EmployeeManagement.Shared.Models;

namespace EmployeeManagement.Core.Services;

public interface IEmployeeService
{
    /// <summary>
    /// Add employee
    /// </summary>
    /// <param name="employee"><see cref="EmployeeModel"/> </param>
    /// <returns></returns>
    Task<Result<EmployeeModel>> AddEmployeeAsync(EmployeeModel employee);

    /// <summary>
    /// Get paged of employees
    /// </summary>
    /// <param name="filter"><see cref="PagingRequest"/></param>
    /// <returns></returns>
    Task<Paging<EmployeeModel>> GetEmployeeListAsync(PagingRequest? filter = null);

    /// <summary>
    /// Get employee by id
    /// </summary>
    /// <param name="id">Employee's id</param>
    /// <returns></returns>
    Task<EmployeeModel> GetEmployeeByIdAsync(int id);

    /// <summary>
    /// Generate new employees 
    /// </summary>
    /// <param name="count">Employee's count</param>
    /// <returns></returns>
    Task<Result<List<EmployeeModel>>> GenerateRandomEmployeesAsync(int count);
}

