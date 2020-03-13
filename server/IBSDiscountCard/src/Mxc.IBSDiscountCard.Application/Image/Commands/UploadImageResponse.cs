using System;

namespace Mxc.IBSDiscountCard.Application.Image.Commands
{
    public class UploadImageResponse
    {
        public string ImageName { get; set; }

        public UploadImageResponse(string imageName)
        {
            ImageName = imageName;
        }
    }
}