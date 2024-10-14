using HRManagement.Domain.Entities;

namespace HRManagement.Domain.DTOs
{
    public class EmployeeDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public ICollection<int> DepartmentIds { get; set; }
    }
}
