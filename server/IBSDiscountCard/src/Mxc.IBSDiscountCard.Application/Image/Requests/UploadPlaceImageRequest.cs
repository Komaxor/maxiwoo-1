using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Requests
{
    public class UploadPlaceImageRequest
    {
        public Guid PlaceId { get; set; }
        public IFormFile File { get; set; }
    }
}
