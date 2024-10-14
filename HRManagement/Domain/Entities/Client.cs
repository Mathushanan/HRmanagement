using System.ComponentModel.DataAnnotations;

namespace HRManagement.Domain.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }

        public ICollection<Department> Departments { get; set; }
    }

}
