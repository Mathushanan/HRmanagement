using HRManagement.Data.Contexts;
using HRManagement.Data.Interfaces;
using HRManagement.Domain.Entities;

namespace HRManagement.Data.Repositories
{
    public class EmployeeDepartmentRepository:IEmployeeDepartmentRepository
    {
        private readonly HRManagementDbContext _context;

        public EmployeeDepartmentRepository(HRManagementDbContext context)
        {
            this._context = context;
        }
        public async Task removeExistingEmployeeDepartments(int employeeId)
        {
            var employeeDepartments = _context.EmployeeDepartments
                                      .Where(ed => ed.EmployeeId == employeeId);

            _context.EmployeeDepartments.RemoveRange(employeeDepartments);
            await _context.SaveChangesAsync();
        }
    }
}
