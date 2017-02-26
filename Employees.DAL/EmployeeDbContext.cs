using Employees.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.DAL
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext()
            : base("name=EmployeeDbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
    }
}
