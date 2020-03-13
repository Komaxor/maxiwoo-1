using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Application.Image.Services;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications;

namespace Mxc.IBSDiscountCard.Application.Place.Queries
{
    public class PlaceQueries : IPlaceQueries
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMapper _mapper;
        private readonly IFileProvider _fIleProvider;

        public PlaceQueries(IPlaceRepository placeRepository, IMapper mapper, IFileProvider fileProvider)
        {
            _placeRepository = placeRepository;
            _fIleProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<PagingList<PlaceHeaderViewModel>> GetPlacesAsync(PagingOptions pagingOptions,
            string searchText)
        {
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> specification = new NotHidden();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                specification = specification.And(new PlaceNameContains(searchText))
                    .Or(new PlaceTypeContains(searchText));
            }

            var places = await _placeRepository.GetOrderedPlacesByMaxDiscountAsync(pagingOptions, specification);

            var viewModels = places.Results.Select(p =>
            {
                return _mapper.Map<PlaceHeaderViewModel>(p);
            });

            return new PagingList<PlaceHeaderViewModel>()
            {
                ResultsLength = places.ResultsLength,
                Results = viewModels
            };
        }

        public async Task<PlaceDetailsViewModel> GetPlaceAsync(Guid placeId)
        {
            var place = await _placeRepository.GetAsync(new IdEquals(new PlaceId(placeId)));

            if (place == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.PlaceNotFoundById);
            }

            return _mapper.Map<PlaceDetailsViewModel>(place);
        }

        public async Task<List<AdminPlaceViewModel>> GetAdminPlacesAsync()
        {
            var places = await _placeRepository.GetAllPlacesAsync();

            return places.Select(place => _mapper.Map<AdminPlaceViewModel>(place)).ToList();
        }

        public async Task<List<PlaceHeaderViewModel>> GetPlacesByCategoryIdAsync(int categoryId)
        {
            var places = await _placeRepository.GetPlacesByCategoryIdAsync(categoryId);

            return places
                .Where(p => _fIleProvider.CheckFileExist(p.PreviewImage))
                .Where(p => !p.IsHidden)
                .Select(place => _mapper.Map<PlaceHeaderViewModel>(place)).ToList();
        }

        public async Task<AdminPlaceViewModel> GetAdminPlaceAsync(Guid placeId)
        {
            var place = await _placeRepository.GetAsync(new IdEquals(new PlaceId(placeId)));

            if (place == null)
            {
                throw new IBSDiscountCardApiErrorException(FunctionCodes.PlaceNotFoundById);
            }

            return _mapper.Map<AdminPlaceViewModel>(place);
        }

        public async Task<List<PlaceHeaderViewModel>> GetPlacesByIdsAsync(List<Guid> placeIds)
        {
            var places = await _placeRepository.GetPlacesByIdsAsync(placeIds);

            return places
                .Where(p => _fIleProvider.CheckFileExist(p.PreviewImage))
                .Where(p => !p.IsHidden)
                .Select(place => _mapper.Map<PlaceHeaderViewModel>(place)).ToList();
        }
    }
}