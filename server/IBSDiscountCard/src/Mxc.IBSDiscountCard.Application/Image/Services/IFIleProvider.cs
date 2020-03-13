using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.Image.Services
{
    public interface IFileProvider
    {
        Task<string> SaveAsync(IFormFile formFile);
        Task<IFileSource> LoadAsync(string fileName);
        void Delete(string fileName);
        bool CheckStorage();
        bool CheckFileExist(string fileName);
    }
}
