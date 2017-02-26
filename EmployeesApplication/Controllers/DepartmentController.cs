using Employees.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeesApplication.Controllers
{
    [RoutePrefix("api/departments"), Authorize]
    public class DepartmentController : ApiController
    {
        private readonly IDepartmentRepository _department;
        public DepartmentController() : this(new DepartmentRepository())
        { 
        }
        public DepartmentController(IDepartmentRepository department) {
            this._department = department;
        }

        protected override void Dispose(bool disposing)
        {
           _department.Dispose();
            base.Dispose(disposing);
        }




        // GET: api/Department
        [Route("")]
        public HttpResponseMessage Get()
        {
            try
            {
                // using generic repository method
                return Request.CreateResponse(HttpStatusCode.OK, _department.GetAll());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        // GET: api/Department/5
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                // using generic repository method
                var model = _department.GetByID(id);

                if (model != null)
                    return Request.CreateResponse(HttpStatusCode.OK, model);
                else
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Department with Id " + id.ToString() + " not found");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

       
    }
}
