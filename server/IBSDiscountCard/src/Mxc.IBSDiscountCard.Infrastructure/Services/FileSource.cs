using Mxc.IBSDiscountCard.Application.Image.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Infrastructure.Services
{
    public class FileSource : IFileSource
    {
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
