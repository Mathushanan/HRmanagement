using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagement.Domain.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int ClientId { get; set; }


        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}
