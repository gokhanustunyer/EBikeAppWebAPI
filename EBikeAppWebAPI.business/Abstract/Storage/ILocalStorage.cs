using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Abstract.Storage
{
    public interface ILocalStorage
    {
        Task<List<(string fileName, string path)>> UploadRangeAsync(string pathOrContainerName, IFormFileCollection files);
        Task<(string fileName, string path)> UploadAsync(string pathOrContainerName, IFormFile files);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
    }
}
