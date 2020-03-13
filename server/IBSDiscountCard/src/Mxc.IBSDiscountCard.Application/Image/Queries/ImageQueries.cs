using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Mxc.IBSDiscountCard.Application.Image.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications;

namespace Mxc.IBSDiscountCard.Application.Image.Queries
{
    public class ImageQueries : IImageQueries
    {
        private readonly IFileProvider _fIleProvider;

        public ImageQueries(IFileProvider fIleProvider)
        {
            _fIleProvider = fIleProvider;
        }

        public async Task<IFileSource> GetImageAsync(string imageName)
        {
            try
            {
                return await _fIleProvider.LoadAsync(imageName);
            }
            catch (FileNotFoundException)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.ImageNotFound);
            }
        }
    }
}
