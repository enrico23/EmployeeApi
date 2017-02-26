using System;
using System.Net.Http;

namespace Employees.Services
{
    public interface IDownloadService : IDisposable
    {
        HttpResponseMessage GetDownload(string path);
    }
}
