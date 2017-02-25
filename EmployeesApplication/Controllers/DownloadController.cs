using EmployeesApplication.Services;
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
        private readonly IDownloadService _download;
        public DownloadController() : this(new DownloadService()) { }
        public DownloadController(IDownloadService download) {
            this._download = download;
        }
        protected override void Dispose(bool disposing)
        {
            _download.Dispose();
            base.Dispose(disposing);
        }

        // GET api/download/5
        public HttpResponseMessage Get(string path)
        {
            return _download.GetDownload(path);
        }
    }
}
