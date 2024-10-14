using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;

namespace HRManagement.Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task AddAsync(EmployeeDto employeeDto);
        Task<Employee> GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> DeleteByIdAsync(int id);
        Task<Employee> UpdateAsync(int id, EmployeeDto updatedEmployeeDto);
    }
}
