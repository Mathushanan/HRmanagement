using HRManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Data.Contexts
{
    public class HRManagementDbContext : DbContext
    {
        public HRManagementDbContext(DbContextOptions<HRManagementDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
        public DbSet<RequestAudit> RequestAudits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Mark client's name as unique
            modelBuilder.Entity<Client>()
            .HasIndex(c => c.Name)
            .IsUnique();

            //Mark department's name as unique
            modelBuilder.Entity<Department>()
            .HasIndex(d => d.Name)
            .IsUnique();

            // M:M relationship between Employee:Department
            modelBuilder.Entity<EmployeeDepartment>()
                .HasKey(ed => new { ed.EmployeeId, ed.DepartmentId });

            modelBuilder.Entity<EmployeeDepartment>()
                .HasOne(ed => ed.Employee)
                .WithMany(e => e.EmployeeDepartments)
                .HasForeignKey(ed => ed.EmployeeId);

            modelBuilder.Entity<EmployeeDepartment>()
                .HasOne(ed => ed.Department)
                .WithMany(d => d.EmployeeDepartments)
                .HasForeignKey(ed => ed.DepartmentId);

            // 1:M relationship between Client:Department
            modelBuilder.Entity<Department>()
                .HasOne(d => d.Client)
                .WithMany(c => c.Departments)
                .HasForeignKey(d => d.ClientId);
        }
    }  
}

