using Microsoft.AspNetCore.Http;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Commands
{
    public class UpdateMyProfileImage : IIBSDiscountCardCommand<UploadImageResponse>
    {
        public IFormFile FormFile { get; private set; }

        public UpdateMyProfileImage(IFormFile formFile)
        {
            FormFile = formFile;
        }
    }
}
