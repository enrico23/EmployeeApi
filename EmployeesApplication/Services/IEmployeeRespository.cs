using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesApplication.Services
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> SelectByGender(string gender);
        bool ExistEmployee(int Id);
        Employee SelectedEmployee(int id);
        void AddEmployee(Employee model);
        Employee EditEmployee(int Id, Employee model);

        int DeleteEmployee(int Id);

        int CreatePdf(string gender, string path);
    }
}
