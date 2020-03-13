using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Validators
{
    public class FileUploadValidator : AbstractValidator<IFormFile>
    {
        public FileUploadValidator(FileUploadOptions uploadOptions)
        {
            RuleFor(u => u.Length).LessThanOrEqualTo(uploadOptions.MaxUploadSize);
        }
    }
}
