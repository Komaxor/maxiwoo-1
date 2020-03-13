using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mxc.Domain.Abstractions.Repositories;

namespace Mxc.IBSDiscountCard.Application.Place.Queries
{
    public interface IPlaceQueries
    {
        Task<PagingList<PlaceHeaderViewModel>> GetPlacesAsync(PagingOptions pagingOptions, string searchText);
        Task<PlaceDetailsViewModel> GetPlaceAsync(Guid placeId);
        Task<List<AdminPlaceViewModel>> GetAdminPlacesAsync();
        Task<List<PlaceHeaderViewModel>> GetPlacesByCategoryIdAsync(int categoryId);
        Task<AdminPlaceViewModel> GetAdminPlaceAsync(Guid placeId);
        Task<List<PlaceHeaderViewModel>> GetPlacesByIdsAsync(List<Guid> placeIds);
    }
}