using HRManagement.Domain.Entities;

namespace HRManagement.Data.Interfaces
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task <Employee>GetByIdAsync(int id);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> DeleteByIdAsync(int id);
        Task<Employee> UpdateAsync(Employee employee);
    }
}
