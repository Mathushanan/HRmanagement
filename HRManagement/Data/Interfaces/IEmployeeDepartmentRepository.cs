using HRManagement.Domain.Entities;

namespace HRManagement.Data.Interfaces
{
    public interface IEmployeeDepartmentRepository
    {
        Task removeExistingEmployeeDepartments(int employeeId);
    }
}
