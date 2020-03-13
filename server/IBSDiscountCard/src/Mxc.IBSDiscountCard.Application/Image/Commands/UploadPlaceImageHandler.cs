using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.Image.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.Image.Commands
{
    public class UploadPlaceImageHandler : IBSDiscountCardCommandHandler<UploadPlaceImage, UploadImageResponse>
    {
        private readonly IPlaceRepository _repository;
        private readonly IFileProvider _fileProvider;

        public UploadPlaceImageHandler(IPlaceRepository repository, IFileProvider fIleProvider, ILogger<UploadPlaceImageHandler> logger) : base(logger)
        {
            _repository = repository;
            _fileProvider = fIleProvider;
        }

        public override async Task<IExecutionResult<UploadImageResponse, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(UploadPlaceImage command, CancellationToken cancellationToken)
        {
            var place = await _repository.GetAsync(new IdEquals(new PlaceId(command.PlaceId)));

            if (place == null)
            {
                return IBSDiscountCardExecutionResult<UploadImageResponse>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.UploadPlaceImagePlaceNotFound));
            }

            var imageName = await _fileProvider.SaveAsync(command.FormFile);

            place.ModifyImage(imageName);

            var isSuccess = await _repository.UpdateAsync(place);

            if (!isSuccess)
            {
                _fileProvider.Delete(imageName);
                return IBSDiscountCardExecutionResult<UploadImageResponse>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.UploadPlaceImageUpdateNotFullfiled));
            }

            return IBSDiscountCardExecutionResult<UploadImageResponse>.FromResult(new UploadImageResponse(imageName));
        }
    }
}
