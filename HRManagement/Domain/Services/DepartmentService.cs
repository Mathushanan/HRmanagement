using HRManagement.Data.Interfaces;
using HRManagement.Data.Repositories;
using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;
using HRManagement.Domain.Interfaces;

namespace HRManagement.Domain.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this._departmentRepository= departmentRepository;   
        }
        public async Task AddAsync(DepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Name = departmentDto.Name,
                ClientId = departmentDto.ClientId

            };
            await _departmentRepository.AddAsync(department);
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);

            return department;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> DeleteByIdAsync(int id)
        {
            return await _departmentRepository.DeleteByIdAsync(id);
        }

        public async Task<Department> UpdateAsync(int id, DepartmentDto departmentDto)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);

            if (existingDepartment == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found");
            }

            existingDepartment.Name = departmentDto.Name;
            existingDepartment.ClientId= departmentDto.ClientId;

            return await _departmentRepository.UpdateAsync(existingDepartment);
        }
    }
}
