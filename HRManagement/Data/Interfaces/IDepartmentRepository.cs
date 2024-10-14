using HRManagement.Domain.Entities;

namespace HRManagement.Data.Interfaces
{
    public interface IDepartmentRepository
    {
        Task AddAsync(Department department);
        Task<Department>GetByIdAsync(int id);
        Task<List<Department>> GetAllAsync();
        Task<Department> DeleteByIdAsync(int id);
        Task<Department> UpdateAsync(Department department);
    }
}
