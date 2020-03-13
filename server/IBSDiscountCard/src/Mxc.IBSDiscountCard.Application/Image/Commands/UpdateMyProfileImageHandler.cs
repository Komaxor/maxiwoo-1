using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Application.Image.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.Image.Commands
{
    public class UpdateMyProfileImageHandler : IBSDiscountCardCommandHandler<UpdateMyProfileImage, UploadImageResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IFileProvider _fileProvider;
        private readonly ILoggedInUserAccessor _loggedInUser;

        public UpdateMyProfileImageHandler(IUserRepository repository, IFileProvider fileProvider, ILoggedInUserAccessor loggedInUser, ILogger<UpdateMyProfileImageHandler> logger) : base(logger)
        {
            _repository = repository;
            _fileProvider = fileProvider;
            _loggedInUser = loggedInUser;
        }

        public override async Task<IExecutionResult<UploadImageResponse, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(UpdateMyProfileImage command, CancellationToken cancellationToken)
        {
            var user = await _repository.GetAsync(new UserNameEquals(_loggedInUser.UserName));

            if (user == null)
            {
                return IBSDiscountCardExecutionResult<UploadImageResponse>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.UploadUserImageUserNotFound));
            }

            var imageName = await _fileProvider.SaveAsync(command.FormFile);

            user.UpdatePhoto(imageName);

            var isSuccess = await _repository.UpdateAsync(user);

            if (!isSuccess)
            {
                _fileProvider.Delete(imageName);
                return IBSDiscountCardExecutionResult<UploadImageResponse>.FromError(new IBSDiscountCardExecutionError(FunctionCodes.UploadUserImageUpdateNotFullfiled));
            }

            return IBSDiscountCardExecutionResult<UploadImageResponse>.FromResult(new UploadImageResponse(imageName));
        }
    }
}
