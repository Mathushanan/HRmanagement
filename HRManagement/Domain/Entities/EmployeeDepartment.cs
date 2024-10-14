using System.ComponentModel.DataAnnotations.Schema;

namespace HRManagement.Domain.Entities
{
    public class EmployeeDepartment
    {
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }

}
