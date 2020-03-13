using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.Domain.Abstractions.Specifications;

namespace Mxc.IBSDiscountCard.Domain.PlaceAggregate
{
    /// <summary>
    /// Place repository
    /// </summary>
    public interface IPlaceRepository : IRepository<Place, PlaceId, Guid, IPlaceSpecificationVisitor>
    {
        /// <summary>
        /// Get a list of places (with cache functionality)
        /// </summary>
        /// <param name="pagingOptions">Paging options</param>
        /// <param name="specification">Filter specifications</param>
        /// <returns></returns>
        Task<PagingList<Domain.PlaceAggregate.Place>> GetOrderedPlacesByMaxDiscountAsync(
            PagingOptions pagingOptions,
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> specification);

        /// <summary>
        /// Get all available place without filtering
        /// </summary>
        /// <returns></returns>
        Task<List<Domain.PlaceAggregate.Place>> GetAllPlacesAsync();
        Task<List<Domain.PlaceAggregate.Place>> GetPlacesByCategoryIdAsync(int categoryId);
        Task<List<Domain.PlaceAggregate.Place>> GetPlacesByIdsAsync(List<Guid> placeIds);
    }
}