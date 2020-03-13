using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Mxc.Commands.Abstractions.Commands;
using Mxc.IBSDiscountCard.Application.Common.Commands;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications;

namespace Mxc.IBSDiscountCard.Application.Place.Commands
{
    public class UpdatePlaceHandler: IBSDiscountCardCommandHandler<UpdatePlace, UpdatePlaceResponse>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;

        public UpdatePlaceHandler(ILogger<UpdatePlaceHandler> logger, IPlaceRepository placeRepository, IMapper mapper) : base(logger)
        {
            _placeRepository = placeRepository;
            _mapper = mapper;
        }

        public override async Task<IExecutionResult<UpdatePlaceResponse, FunctionCodes, IBSDiscountCardExecutionError>> HandleAsync(UpdatePlace command, CancellationToken cancellationToken)
        {
            var place = await _placeRepository.GetAsync(new IdEquals(new PlaceId(command.Id)));

            var openingHours = command.OpeningHours;
            
            place.Update(command.Name,
                command.Type,
                command.About,
                new OpeningHoursOfDay(openingHours.Monday, openingHours.Tuesday, openingHours.Wednesday, openingHours.Thursday, openingHours.Friday, openingHours.Saturday, openingHours.Sunday), 
                new Address(command.Address.DisplayAddress, command.Address.Latitude, command.Address.Longitude), //TODO cserélni
                command.Tags,
                command.IsHidden,
                command.CategoryId,
                command.PhoneNumber,
                command.Email,
                command.Web,
                command.Facebook,
                command.Instagram);

            await _placeRepository.UpdateAsync(place);
            
            return IBSDiscountCardExecutionResult<UpdatePlaceResponse>.FromResult(new UpdatePlaceResponse());
        }
    }
}