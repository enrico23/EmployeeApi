using Employees.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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


        public int CreatePdf(string gender, string path)
        {
           

            try
            {
                var vFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/" + path);
                var employees = this.SelectByGender(gender);
                if (employees == null)
                {
                    throw new Exception();
                }



                using (FileStream fs = new FileStream(vFolderPath, FileMode.Create))
                {

                    Document document = new Document(PageSize.A4, 25, 25, 30, 30);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);

                    document.AddAuthor("Enrico Acampora");
                    document.AddCreator("Enrico Acampora");
                    document.AddKeywords("Employee List");
                    document.AddSubject("Employee List");
                    document.AddTitle("Company Employee List");
                    document.Open();



                    PdfPTable table = new PdfPTable(3);
                    PdfPCell cell = new PdfPCell(new Phrase("Company Employee List", new Font(Font.FontFamily.HELVETICA, 20f, Font.BOLD)));
                    cell.Colspan = 3;
                    cell.PaddingTop = 6f;
                    cell.PaddingBottom = 6f;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    foreach (var g in employees.GroupBy(s => s.LastName.First()).OrderBy(g => g.Key))
                    {

                        cell = new PdfPCell(new Phrase(g.Key.ToString(), new Font(Font.FontFamily.HELVETICA, 24f, Font.BOLD)));
                        cell.Colspan = 3;
                        cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        cell.PaddingTop = 6f;
                        cell.PaddingBottom = 6f;
                        table.AddCell(cell);



                        foreach (var item in g.OrderBy(s => s.LastName))
                        {
                            cell = new PdfPCell(new Phrase(item.LastName + " " + item.FirstName, new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.NORMAL)));
                            cell.PaddingTop = 6f;
                            cell.PaddingBottom = 6f;
                            table.AddCell(cell);


                            cell = new PdfPCell(new Phrase(item.Gender, new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.NORMAL)));
                            cell.PaddingTop = 6f;
                            cell.PaddingBottom = 6f;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("£ " + item.Salary.ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 12f, Font.NORMAL)));
                            cell.PaddingTop = 6f;
                            cell.PaddingBottom = 6f;
                            table.AddCell(cell);


                        }

                    }

                    document.Add(table);

                    document.Close();
                    writer.Close();
                }

                return 1;
            }
            catch (Exception /*ex*/)
            {
                return 0;

            }




        }
    }
}