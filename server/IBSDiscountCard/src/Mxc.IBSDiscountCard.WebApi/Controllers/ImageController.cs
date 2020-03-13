using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mxc.IBSDiscountCard.Application.Image.Commands;
using Mxc.IBSDiscountCard.Application.Image.Queries;
using Mxc.IBSDiscountCard.Application.Image.Requests;
using Mxc.IBSDiscountCard.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    public class ImageController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageQueries _imageQueries;

        public ImageController(IMediator mediator, IImageQueries imageQueries)
        {
            _mediator = mediator;
            _imageQueries = imageQueries;
        }

        /// <summary>
        /// Upload image for place
        /// </summary>
        /// <param name="request">Image content</param>
        /// <returns>Image id</returns>
        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [Route("place")]
        public async Task<UploadImageResponse> UploadPlaceImageAsync([FromForm] UploadPlaceImageRequest request)
        {
            var response = await _mediator.Send(new UploadPlaceImage(request.PlaceId, request.File));

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Upload image for user
        /// </summary>
        /// <param name="request">Image content</param>
        /// <returns>Image id</returns>
        [HttpPost]
        [Route("user")]
        public async Task<UploadImageResponse> UploadUserImageAsync([FromForm] UpdateMyProfileImageRequest request)
        {
            var response = await _mediator.Send(new UpdateMyProfileImage(request.File));

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }

        /// <summary>
        /// Get image by id
        /// </summary>
        /// <param name="imageName">Image id</param>
        /// <returns>Image</returns>
        ///
        [AllowAnonymous]
        [HttpGet("{imageName}")]
        public async Task<IActionResult> GetAsync(string imageName)
        {
            var fileSource = await _imageQueries.GetImageAsync(imageName);

            return File(fileSource.Content, fileSource.ContentType);
        }
    }
}