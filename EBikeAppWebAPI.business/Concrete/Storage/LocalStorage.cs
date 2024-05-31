using EBikeAppWebAPI.business.Abstract.Service;
using EBikeAppWebAPI.business.Abstract.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBikeAppWebAPI.business.Concrete.Storage
{
    public class LocalStorage : ILocalStorage
    {
        readonly IHostEnvironment _webHostEnvironment;
        public LocalStorage(IHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string path, string fileName)
            => File.Delete($"{path}\\{fileName}");

        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(f => f.Name).ToList();
        }

        public async Task<(string fileName, string path)> UploadAsync(string path, IFormFile file)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", path);
            if (!Directory.Exists(uploadPath)) { Directory.CreateDirectory(uploadPath); }

            List<(string fileName, string path)> datas = new();
            string extension = Path.GetExtension(file.FileName);
            string newFileName = Guid.NewGuid().ToString() + extension;
            await CopyFileAsync($"{uploadPath}\\{newFileName}", file);

            return (newFileName, $"{path}\\{newFileName}");
        }

        public async Task<List<(string fileName, string path)>> UploadRangeAsync(string path, IFormFileCollection files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot", path);
            if (!Directory.Exists(uploadPath)) { Directory.CreateDirectory(uploadPath); }

            List<bool> results = new();
            List<(string fileName, string path)> datas = new();
            foreach (IFormFile file in files)
            {
                string extension = Path.GetExtension(file.FileName);
                string newFileName = Guid.NewGuid().ToString() + extension;
                await CopyFileAsync($"{uploadPath}\\{newFileName}", file);
                datas.Add((newFileName, $"{path}\\{newFileName}"));
            }

            return datas;
        }

        private async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using (FileStream fileStream = new(path, FileMode.Create,
                    FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false))
                {
                    await file.CopyToAsync(fileStream);
                    await fileStream.FlushAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}