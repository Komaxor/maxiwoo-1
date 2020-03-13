using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Requests
{
    public class UpdateMyProfileImageRequest
    {
        public IFormFile File { get; set; }
    }
}
