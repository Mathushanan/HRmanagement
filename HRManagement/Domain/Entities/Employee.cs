using System.ComponentModel.DataAnnotations;

namespace HRManagement.Domain.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }= new List<EmployeeDepartment>();
    }
}
