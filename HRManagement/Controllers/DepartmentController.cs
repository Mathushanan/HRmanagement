using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;
using HRManagement.Domain.Interfaces;
using HRManagement.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentDto departmentDto)
        {
            if (departmentDto == null)
            {
                return BadRequest("Department data is null!");
            }
            else
            {
                try
                {
                    await _departmentService.AddAsync(departmentDto);
                    return Ok("Department Added!");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetByIdAsync(id);
                if (department == null)
                {
                    return NotFound("Department not found!");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departmentList = await _departmentService.GetAllAsync();
                if (departmentList == null)
                {
                    return NotFound("Department table is empty!");
                }
                return Ok(departmentList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.DeleteByIdAsync(id);
                if (department == null)
                {
                    return NotFound("Department not found!");
                }
                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] DepartmentDto updatedDepartmentDto)
        {
            if (updatedDepartmentDto == null)
            {
                return BadRequest("Department data cannot be null.");
            }

            try
            {
                var result = await _departmentService.UpdateAsync(id, updatedDepartmentDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
