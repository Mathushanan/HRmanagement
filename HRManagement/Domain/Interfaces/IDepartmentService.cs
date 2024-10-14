using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;

namespace HRManagement.Domain.Interfaces
{
    public interface IDepartmentService
    {
        Task AddAsync(DepartmentDto departmentDto);
        Task<Department> GetByIdAsync(int id);
        Task<List<Department>> GetAllAsync();
        Task<Department> DeleteByIdAsync(int id);
        Task<Department> UpdateAsync(int id, DepartmentDto updatedDepartmentDto);

    }
}
