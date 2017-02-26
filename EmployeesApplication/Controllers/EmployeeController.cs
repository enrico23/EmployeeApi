using Employees.Models;
using Employees.Services;
using EmployeesApplication.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace EmployeesApplication.Controllers
{
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        private readonly  IEmployeeRepository _employee;
        private readonly IDownloadService _download;

        

        public EmployeeController(IEmployeeRepository employee, IDownloadService download)
        {
            this._employee = employee;
            this._download = download;
        }

        protected override void Dispose(bool disposing)
        {
            _employee.Dispose();
            _download.Dispose();
            base.Dispose(disposing);
        }

        #region API_METHODS


        // GET: api/Employee
        /// <summary>
        /// Full List with details
        /// </summary>
        /// <returns>Ienumerable<Employee></returns>
        [Route("")]
        public HttpResponseMessage GetEmployees()
        {
            try
            {
                // using generic repository method
                return Request.CreateResponse(HttpStatusCode.OK, _employee.GetAll());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            
        }

        // GET: api/Employee/5
        /// <summary>
        /// Get Employee by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{id:int}")] 
        public HttpResponseMessage GetEmployeesById(int id)
        {
            try
            {
                // using generic repository method
                var model = _employee.GetByID(id);

                if (model != null)
                    return Request.CreateResponse(HttpStatusCode.OK, model);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id.ToString() + " not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
            
        }

        // GET: api/Employee/female
        /// <summary>
        /// Get employee by gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns>Ienumerable<Employee></returns>
        [Route("{gender}")]
        public HttpResponseMessage GetEmployeeByGender(string gender)
        {
            try
            {
                var employees = _employee.SelectByGender(gender);
                if (employees == null) {
                    throw new Exception();
                }

                return Request.CreateResponse(HttpStatusCode.OK, employees);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

        }

        [Route("{gender}/basic")]
        [BasicAuthentication]
        public HttpResponseMessage GetEmployeeByGenderBasicAuth(string gender = "all")
        {
            try
            {
                string username = Thread.CurrentPrincipal.Identity.Name;

                var employees = _employee.SelectByGender(username);
                if (employees == null)
                {
                    throw new Exception();
                }

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

        }

        /// <summary>
        /// TO IMPLEMENT
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        [Route("{gender}/pdf")]
        public HttpResponseMessage GetEmployeePdf(string gender = "all")
        {
            try
            {
                

                string pdfName = "EmployeeList.pdf";
                int result = _employee.CreatePdf(gender, pdfName);
                if (result == 0)
                {
                    throw new Exception();
                }

                return _download.GetDownload(pdfName);


               

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

        }



        // POST: api/Employee
        public HttpResponseMessage Post([FromBody]Employee model)
        {
            try
            {
               _employee.AddEmployee(model);
               
               var message = Request.CreateResponse(HttpStatusCode.Created, model);
               message.Headers.Location = new Uri(Request.RequestUri + model.Id.ToString());
               return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Employee/5
        public HttpResponseMessage Put(int id, [FromBody]Employee model)
        {
            try
            {
                Employee updatedEmployee = _employee.EditEmployee(id, model);
                if (updatedEmployee == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "not such employee");
                else
                    return Request.CreateResponse(HttpStatusCode.OK, updatedEmployee);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        // DELETE: api/Employee/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                int result = _employee.DeleteEmployee(id);
                if (result == 0)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found");
                else
                    return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
           
        }


        #endregion
    }
}
