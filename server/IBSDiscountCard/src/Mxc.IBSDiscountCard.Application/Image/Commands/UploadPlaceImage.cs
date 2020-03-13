using Microsoft.AspNetCore.Http;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Commands
{
    public class UploadPlaceImage : IIBSDiscountCardCommand<UploadImageResponse>
    {
        public Guid PlaceId { get; private set; }
        public IFormFile FormFile { get; private set; }

        public UploadPlaceImage(Guid placeId, IFormFile formFile)
        {
            PlaceId = placeId;
            FormFile = formFile;
        }
    }
}
