using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Requests
{
    public class UploadUserImageRequest
    {
        public string UserName { get; set; }
        public IFormFile File { get; set; }
    }
}
