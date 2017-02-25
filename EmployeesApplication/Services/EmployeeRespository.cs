using Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesApplication.Services
{
    public class EmployeeRespository: 
        GenericRepository<EmployeeDbEntities, Employee>, 
        IEmployeeRepository
    {
        public IEnumerable<Employee> SelectByGender(string gender) {
            if (string.IsNullOrEmpty(gender))
            {

                return null;
            }
            else
            {
                switch (gender.ToLower())
                {
                    case "male":
                        return this.Get(s => s.Gender.ToLower() == "male");

                    case "female":
                        return this.Get(s => s.Gender.ToLower() == "female");
                    default:
                        return this.Get();
                }
            }
            

        }

        public bool ExistEmployee(int Id)
        {
            return base.Get(s => s.Id == Id) !=null;
        }

        public Employee SelectedEmployee(int Id) {
            return base.Get(s => s.Id == Id).SingleOrDefault();
        }

        public void AddEmployee(Employee model)
        {
            base.Add(model);
            base.Save();
        }


        public Employee EditEmployee(int Id, Employee model)
        {

            var selectedEmployee = this.SelectedEmployee(Id);

            if (selectedEmployee != null)
            {
                selectedEmployee.FirstName = model.FirstName;
                selectedEmployee.LastName = model.LastName;
                selectedEmployee.Gender = model.Gender;
                selectedEmployee.Salary = model.Salary;

                base.Edit(selectedEmployee);
                base.Save();

                return selectedEmployee;

            }
            return null;
            
        }

        public int DeleteEmployee(int Id)
        {
            var selectedEmployee = this.SelectedEmployee(Id);
            if (selectedEmployee != null)
            {
                base.Delete(selectedEmployee);
                base.Save();
                return 1;
            }
            return 0;
            
        }
    }
}