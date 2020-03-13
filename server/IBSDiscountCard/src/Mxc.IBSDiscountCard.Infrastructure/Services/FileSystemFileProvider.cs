using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MimeMapping;
using Mxc.IBSDiscountCard.Application.Image.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Infrastructure.Services
{
    public class FileSystemFileProvider : IFileProvider
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public string BasePath => Path.Combine(_hostingEnvironment.WebRootPath, "ibsImages");

        public FileSystemFileProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IFileSource> LoadAsync(string fileName)
        {
            var path = GetLocalFilePath(fileName);
            byte[] fileBytes = await File.ReadAllBytesAsync(path);

            return new FileSource()
            {
                Content = fileBytes,
                ContentType = MimeUtility.GetMimeMapping(path)
            };
        }

        public bool CheckFileExist(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return false;
            }

            var path = Path.Combine(BasePath, fileName);
            return File.Exists(path);
        }

        public async Task<string> SaveAsync(IFormFile formFile)
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            var guid = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(formFile.FileName);
            var filename = Path.ChangeExtension(guid, extension);
            var path = Path.Combine(BasePath, filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            return filename;
        }

        public void Delete(string fileName)
        {
            var path = GetLocalFilePath(fileName);
            File.Delete(path);
        }

        private string GetLocalFilePath(string fileName)
        {
            var path = Path.Combine(BasePath, fileName);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(fileName);
            }

            return path;
        }

        public bool CheckStorage()
        {
            if (_hostingEnvironment.WebRootPath == null || !Directory.Exists(_hostingEnvironment.WebRootPath))
            {
                return false;
            }

            return true;
        }
    }
}
