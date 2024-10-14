using HRManagement.Data.Interfaces;
using HRManagement.Data.Repositories;
using HRManagement.Domain.DTOs;
using HRManagement.Domain.Entities;
using HRManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Domain.Services
{
    public class EmployeeService : IEmployeeService

    {
        private IEmployeeRepository _employeeRepository;
        private IEmployeeDepartmentRepository _employeeDepartmentRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IEmployeeDepartmentRepository employeeDepartmentRepository)
        {
            this._employeeRepository = employeeRepository;
            this._employeeDepartmentRepository= employeeDepartmentRepository; ;

        }
        public async Task AddAsync(EmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                EmployeeDepartments = employeeDto.DepartmentIds.Select(departmentId => new EmployeeDepartment
                {
                    DepartmentId = departmentId

                }).ToList()
                
            };

            await _employeeRepository.AddAsync(employee);
        }

        public async Task<Employee>GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return employee;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();

        }

        public async Task<Employee> DeleteByIdAsync(int id)
        {
            return await _employeeRepository.DeleteByIdAsync(id);
        }

        public async Task<Employee> UpdateAsync(int id, EmployeeDto updatedEmployeeDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            employee.FirstName = updatedEmployeeDto.FirstName;
            employee.LastName = updatedEmployeeDto.LastName;

            if (updatedEmployeeDto.DepartmentIds != null)
            {
               await _employeeDepartmentRepository.removeExistingEmployeeDepartments(id);

                foreach (var departmentId in updatedEmployeeDto.DepartmentIds)
                {
                        employee.EmployeeDepartments.Add(new EmployeeDepartment
                        {
                            EmployeeId = employee.Id,
                            DepartmentId = departmentId
                        });
                }
            }

            return await _employeeRepository.UpdateAsync(employee);
        }






    }
}
