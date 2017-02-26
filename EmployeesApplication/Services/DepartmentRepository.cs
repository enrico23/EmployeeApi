using Employees.DAL;
using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesApplication.Services
{
    
    public class DepartmentRepository : 
        GenericRepository<EmployeeDbContext, Department>, 
        IDepartmentRepository
    {
    }
}