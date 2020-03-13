using System;
using System.Collections.Generic;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Place
{
    public interface IPlaceCache
    {
        List<PlaceDb> Get(Guid institueId);
        void Set(Guid institueId, List<PlaceDb> places);
        void Invalidate();
    }
}