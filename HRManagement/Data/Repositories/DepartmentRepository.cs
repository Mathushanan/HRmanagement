using HRManagement.Data.Contexts;
using HRManagement.Data.Interfaces;
using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Repositories
{
    public class DepartmentRepository:IDepartmentRepository
    {
        private readonly HRManagementDbContext _context;

        public DepartmentRepository(HRManagementDbContext context)
        {
            this._context = context;
        }

        public async Task AddAsync(Department department)
        {
            await _context.Set<Department>().AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Set<Department>().ToListAsync();

        }
        public async Task<Department> DeleteByIdAsync(int id)
        {
            var department = await _context.Set<Department>().FindAsync(id);
            
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }

            _context.Set<Department>().Remove(department);
            await _context.SaveChangesAsync();

            return department;
        }

        public async Task<Department> UpdateAsync(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return department;
        }
    }
}
