using EmployeeManagement.Core.Common;
using EmployeeManagement.Core.Services;
using EmployeeManagement.Shared.Models;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagement.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController(IEmployeeService employeeService) : ControllerBase
    {
        private readonly IEmployeeService employeeService = employeeService;

        /// <summary>
        /// Get list of employee
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-employees")]
        public async Task<IActionResult> GetEmployeeList([FromQuery] PagingRequest filter)
        {
            var list = await employeeService.GetEmployeeListAsync(filter ?? new());
            return Ok(list);
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id">Employee's id</param>
        /// <returns></returns>
        [HttpGet("get-employee/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var user = await employeeService.GetEmployeeByIdAsync(id);
            return Ok(user);
        }

        /// <summary>
        /// Add employee
        /// </summary>
        /// <param name="employee"><see cref="EmployeeModel"/></param>
        /// <returns></returns>
        [HttpPost("add-employee")]
        public async Task<IActionResult> AddEmployee(EmployeeModel employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(employee);
            }

            var result = await employeeService.AddEmployeeAsync(employee);
            return Ok(result);
        }

        /// <summary>
        /// Generate new employees 
        /// </summary>
        /// <param name="count">Employee's count</param>
        /// <returns></returns>
        [HttpPost("generate-random-employees")]
        public async Task<IActionResult> GenerateRandomEmployees(int count)
        {
            var result = await employeeService.GenerateRandomEmployeesAsync(count);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Code.ToString());
        }

    }
}
