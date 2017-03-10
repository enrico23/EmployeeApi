using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EmployeesApplication.Controllers
{
    public class Student {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StudentController : ApiController
    {
        public static List<Student> students = new List<Student>(){
            new Student { Id = 1, Name = "John" },
            new Student { Id = 2, Name = "Jane" },
            new Student { Id = 3, Name = "Frank" }
        };
        
        
    
        // GET: api/Test
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, students);
        }

        // GET: api/Test/5
        public HttpResponseMessage Get(int id)
        {
           return Request.CreateResponse(HttpStatusCode.OK, students.SingleOrDefault(s=> s.Id == id));
        }

        // POST: api/Test
        public HttpResponseMessage Post([FromBody]Student newstudent)
        {
           
            int newid = students.Count() + 1;
            newstudent.Id = newid;
            students.Add(newstudent);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT: api/Test/5
        public HttpResponseMessage Put(int id, [FromBody]Student studentToUpdate)
        {
            
            students.SingleOrDefault(s => s.Id == id).Name = studentToUpdate.Name;
            return Request.CreateResponse(HttpStatusCode.OK);

            
        }

        // DELETE: api/Test/5
        public HttpResponseMessage Delete(int id)
        {
            var index = students.FindIndex(s => s.Id == id);
            students.RemoveAt(index);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("OPTIONS")]
        public HttpResponseMessage Options()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Headers.Add("Access-Control-Allow-Origin", "*");
            resp.Headers.Add("Access-Control-Allow-Methods", "GET,DELETE");

            return resp;
        }
    }
}
