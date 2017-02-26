using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Employees.Services
{
    public class DownloadService : IDownloadService
    {
        public System.Net.Http.HttpResponseMessage GetDownload(string path)
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
            catch (Exception /*ex*/)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return response;
            }
        }

        #region dispose

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {

            if (!this.disposed)
                if (disposing)
                    // dispose dependencies

                    this.disposed = true;
        }

        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
