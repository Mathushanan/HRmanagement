using HRManagement.Data.Contexts;
using HRManagement.Data.Interfaces;
using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HRManagementDbContext _context;
        
        public EmployeeRepository(HRManagementDbContext context)
        {
            this._context = context; 
        }
        public async Task AddAsync(Employee employee)
        {
            await _context.Set<Employee>().AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            var employee=await _context.Employees
                .FirstOrDefaultAsync(e=>e.Id==id);
            
            return employee;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Set<Employee>().ToListAsync();
        }

        public async Task<Employee> DeleteByIdAsync(int id)
        {
            var employee = await _context.Set<Employee>().FindAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            _context.Set<Employee>().Remove(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return employee;
        }
    }
}
