using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Mxc.IBSDiscountCard.Application.Image.Requests;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Image.Validators
{
    public class UploadPlaceImageRequestValidator : AbstractValidator<UploadPlaceImageRequest>
    {
        private readonly FileUploadOptions _uploadOptions;

        public UploadPlaceImageRequestValidator(IOptions<FileUploadOptions> options)
        {
            _uploadOptions = options.Value;

            RuleFor(u => u.PlaceId).NotEmpty();
            RuleFor(u => u.File).NotEmpty().SetValidator(new FileUploadValidator(_uploadOptions));
            RuleFor(u => u.File.ContentType).Must(t => t.EndsWith("jpeg") || t.EndsWith("jpg") || t.EndsWith("png")).WithMessage("File type should be 'jpeg/jpg/png'.");
        }
    }
}
