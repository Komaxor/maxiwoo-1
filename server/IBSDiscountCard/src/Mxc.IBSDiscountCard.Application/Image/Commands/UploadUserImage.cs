using Microsoft.AspNetCore.Http;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Commands
{
    public class UploadUserImage : IIBSDiscountCardCommand<UploadImageResponse>
    {
        public string UserName { get; private set; }
        public IFormFile FormFile { get; private set; }

        public UploadUserImage(string userName, IFormFile formFile)
        {
            UserName = userName;
            FormFile = formFile;
        }
    }
}
