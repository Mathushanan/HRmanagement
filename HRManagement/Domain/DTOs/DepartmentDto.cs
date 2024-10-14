using HRManagement.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRManagement.Domain.DTOs
{
    public class DepartmentDto
    {
        public int Id { get; set; } 
        public required string Name { get; set; }
        public int ClientId { get; set; }
    }
}
