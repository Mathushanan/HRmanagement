using HRManagement.Domain.DTOs;
using HRManagement.Domain.Interfaces;
using HRManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;   
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeDto employeeDto)
        {
            if (employeeDto == null)
            {
                return BadRequest("Employee data is null!");
            }
            else
            {
                try
                {
                    await _employeeService.AddAsync(employeeDto);
                    return Ok("Employee added!");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult>GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found!");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employeeList = await _employeeService.GetAllAsync();
                if (employeeList == null)
                {
                    return NotFound("Employee table is empty!");
                }
                return Ok(employeeList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.DeleteByIdAsync(id);
                if (employee == null)
                {
                    return NotFound("Employee not found!");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto updatedEmployeeDto)
        {
            if (updatedEmployeeDto == null)
            {
                return BadRequest("Employee data cannot be null.");
            }

            try
            {
                var result = await _employeeService.UpdateAsync(id, updatedEmployeeDto);
                return Ok("Employee updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
