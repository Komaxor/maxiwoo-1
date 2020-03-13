using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.IBSDiscountCard.Application.Place.Commands;
using Mxc.IBSDiscountCard.Application.Place.Queries;
using Mxc.IBSDiscountCard.Common;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    public class PlaceController : ApiControllerBase
    {
        private readonly IPlaceQueries _placeQueries;
        private readonly IMediator _mediator;

        public PlaceController(IPlaceQueries placeQueries, IMediator mediator)
        {
            _placeQueries = placeQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// Get place list
        /// </summary>
        /// <param name="pagingOptions">Options for pagination</param>
        /// <param name="searchText">Optional search parameter</param>
        /// <returns>Place header list</returns>
        [HttpGet]
        public Task<PagingList<PlaceHeaderViewModel>> GetPlacesAsync([FromQuery] PagingOptions pagingOptions,
            string searchText)
        {
            return _placeQueries.GetPlacesAsync(pagingOptions, searchText);
        }

        [AllowAnonymous]
        [HttpGet("category/{categoryId:int}")]
        public Task<List<PlaceHeaderViewModel>> GetPlacesAsync( int categoryId)
        {
            return _placeQueries.GetPlacesByCategoryIdAsync(categoryId);
        }

        /// <summary>
        /// Get place by id
        /// </summary>
        /// <param name="placeId">Place id</param>
        /// <returns>Place details</returns>
        [AllowAnonymous]
        [HttpGet("{placeId}")]
        public Task<PlaceDetailsViewModel> GetPlaceAsync(Guid placeId)
        {
            return _placeQueries.GetPlaceAsync(placeId);
        }

        [AllowAnonymous]
        [HttpPost("{favourite}")]
        public Task<List<PlaceHeaderViewModel>> GetPlacesAsync(List<Guid> placeIds)
        {
            return _placeQueries.GetPlacesByIdsAsync(placeIds);
        }

        /// <summary>
        /// Create new place
        /// </summary>
        /// <param name="command">Place data</param>
        /// <returns>Place Id</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<AddPlaceResponse> PostPlaceAsync([FromBody] AddPlace command)
        {
            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return response.Result;
            }

            throw response.ExecutionError.ToApiErrorException();
        }
    }
}