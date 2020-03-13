using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Application.Place.Commands
{
    public class AddPlaceResponse
    {
        public Guid PlaceId { get; set; }

        public AddPlaceResponse(Guid placeId)
        {
            PlaceId = placeId;
        }
    }
}
