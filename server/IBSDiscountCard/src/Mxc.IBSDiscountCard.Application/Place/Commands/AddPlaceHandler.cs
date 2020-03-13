using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Application.Place.Commands
{
    public class AddPlaceHandler : IBSDiscountCardCommandHandler<AddPlace, AddPlaceResponse>
    {
        private readonly IPlaceRepository _repository;

        public AddPlaceHandler(IPlaceRepository repository, ILogger<AddPlaceHandler> logger) : base(logger)
        {
            _repository = repository;
        }

        public override async Task<IExecutionResult<AddPlaceResponse, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(AddPlace command, CancellationToken cancellationToken)
        {
            var openingHours = command.OpeningHours;

            var address = new Address(command.Address.DisplayAddress, command.Address.Latitude, command.Address.Longitude);

            var place = new Domain.PlaceAggregate.Place(
                command.Name,
                command.Type,
                command.About,
                new OpeningHoursOfDay(openingHours.Monday, openingHours.Tuesday, openingHours.Wednesday, openingHours.Thursday, openingHours.Friday, openingHours.Saturday, openingHours.Sunday), 
                command.PreviewImage,
                address,
                command.Tags,
                command.IsHidden,
                command.CategoryId,
                command.PhoneNumber,
                command.Email,
                command.Web,
                command.Facebook,
                command.Instagram);

            var result = await _repository.InsertAsync(place);

            return IBSDiscountCardExecutionResult<AddPlaceResponse>.FromResult(new AddPlaceResponse(result.Id.Id));
        }
    }
}
