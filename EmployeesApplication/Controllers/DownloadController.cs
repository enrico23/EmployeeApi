using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace EmployeesApplication.Controllers
{
    public class DownloadController : ApiController
    {
        //public DownloadController() { }
        //public DownloadController() { }

        // GET api/download/5
        public HttpResponseMessage Get(string path)
        {

            try
            {
                string ext;

                if (path == null)
                {
                    throw new ArgumentNullException("Path is null");
                }
                ext = Path.GetExtension(path);

                string contentType = "";
                var vFolderPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Downloads/" + path);
                switch (ext)
                {
                    case ".txt":
                        contentType = "application/octet-stream";
                        break;
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".doc":
                        contentType = "application/msword";
                        break;
                    case ".docx":
                        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                }
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(File.OpenRead(vFolderPath));
                response.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = path
                };
                return response;

            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }


           


        }
    }
}
