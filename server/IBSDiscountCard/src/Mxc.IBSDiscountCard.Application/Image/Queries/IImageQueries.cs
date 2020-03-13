using Mxc.IBSDiscountCard.Application.Image.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.Image.Queries
{
    public interface IImageQueries
    {
        Task<IFileSource> GetImageAsync(string imageName);
    }
}
