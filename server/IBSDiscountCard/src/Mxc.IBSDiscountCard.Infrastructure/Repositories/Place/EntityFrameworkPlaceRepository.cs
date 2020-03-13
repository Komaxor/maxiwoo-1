using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Place
{
    public class EntityFrameworkPlaceRepository : IPlaceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILoggedInUserAccessor _loggedInUser;
        private readonly IPlaceCache _cache;

        public EntityFrameworkPlaceRepository(ApplicationDbContext db, IMapper mapper, ILoggedInUserAccessor loggedInUser, IPlaceCache cache)
        {
            _db = db;
            _mapper = mapper;
            _loggedInUser = loggedInUser;
            _cache = cache;
        }

        public async Task<Domain.PlaceAggregate.Place> GetAsync(
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> specification)
        {
            var placeDb = await _db.Places.SingleOrDefaultAsync(specification
                .ToFilterExpression<Domain.PlaceAggregate.Place, ExpressionPlaceSpecificationVisitor,
                    IPlaceSpecificationVisitor, PlaceDb>());

            return _mapper.Map<Domain.PlaceAggregate.Place>(placeDb);
        }

        public async Task<PagingList<Domain.PlaceAggregate.Place>> GetOrderedPlacesByMaxDiscountAsync(
            PagingOptions pagingOptions,
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> specification)
        {
            IEnumerable<PlaceDb> query = _cache.Get(_loggedInUser.InstitueId)?.ToList();

            if (query == null)
            {
                var places = await _db.Places.ToListAsync();

                places = places.OrderByDescending(p => p.Name).ToList();

                _cache.Set(_loggedInUser.InstitueId, places);
                
                query = places;
            }

            query = query.Where(specification
                .ToFilterExpression<Domain.PlaceAggregate.Place, ExpressionPlaceSpecificationVisitor,
                    IPlaceSpecificationVisitor, PlaceDb>().Compile());

            var count = query.Count();

            if (pagingOptions?.Limit != null)
            {
                query = query.Skip(pagingOptions.PageIndex * pagingOptions.Limit.Value).Take(pagingOptions.Limit.Value);
            }

            var dbResults = query.ToList();

            return new PagingList<Domain.PlaceAggregate.Place>()
            {
                Results = dbResults.Select(_mapper.Map<Domain.PlaceAggregate.Place>),
                ResultsLength = count
            };
        }

        public async Task<PagingList<Domain.PlaceAggregate.Place>> FindAsync(
            PagingOptions pagingOptions,
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> specification,
            params ISorterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor>[] sorterSpecifications)
        {
            var query = _db.Places.Where(specification
                .ToFilterExpression<Domain.PlaceAggregate.Place, ExpressionPlaceSpecificationVisitor,
                    IPlaceSpecificationVisitor, PlaceDb>());

            var count = query.Count();

            foreach (var sorterSpecification in sorterSpecifications.Reverse())
            {
                var sorterExpression = sorterSpecification
                    .ToSorterExpression<Domain.PlaceAggregate.Place, ExpressionPlaceSpecificationVisitor,
                        IPlaceSpecificationVisitor, PlaceDb>();

                query = sorterSpecification.IsAsc
                    ? query.OrderBy(sorterExpression)
                    : query.OrderByDescending(sorterExpression);
            }

            if (pagingOptions?.Limit != null)
            {
                query = query.Skip(pagingOptions.PageIndex * pagingOptions.Limit.Value).Take(pagingOptions.Limit.Value);
            }

            var dbResults = await query.ToListAsync();

            return new PagingList<Domain.PlaceAggregate.Place>()
            {
                Results = dbResults.Select(_mapper.Map<Domain.PlaceAggregate.Place>),
                ResultsLength = count
            };
        }

        public async Task<List<Domain.PlaceAggregate.Place>> GetAllPlacesAsync()
        {
            var places = await _db.Places.ToListAsync();
            return places.Select(_mapper.Map<Domain.PlaceAggregate.Place>).ToList();
        }

        public async Task<Domain.PlaceAggregate.Place> InsertAsync(Domain.PlaceAggregate.Place item)
        {
            var dbItem = _mapper.Map<PlaceDb>(item);

            _db.Places.Add(dbItem);
            await _db.SaveChangesAsync();
            
            return item;
        }

        public Task<IEnumerable<Domain.PlaceAggregate.Place>> InsertAsync(
            IEnumerable<Domain.PlaceAggregate.Place> items)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(Domain.PlaceAggregate.Place item)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(PlaceId key)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(IEnumerable<Domain.PlaceAggregate.Place> items)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAsync(IEnumerable<PlaceId> keys)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Domain.PlaceAggregate.Place item,
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> guardSpecification = null)
        {
            var query = GetBaseQuery();

            if (guardSpecification != null)
            {
                query = query.Where(guardSpecification
                    .ToFilterExpression<Domain.PlaceAggregate.Place, ExpressionPlaceSpecificationVisitor,
                        IPlaceSpecificationVisitor, PlaceDb>());
            }

            var db = await query.SingleOrDefaultAsync(a => a.Id == item.Id.Id);

            _mapper.Map(item, db);

            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public Task<long> CountAsync(
            IFilterSpecification<Domain.PlaceAggregate.Place, IPlaceSpecificationVisitor> specification)
        {
            throw new System.NotImplementedException();
        }

        private IQueryable<PlaceDb> GetBaseQuery()
        {
            return _db.Places.Include(a => a.Address);
        }

        public async Task<List<Domain.PlaceAggregate.Place>> GetPlacesByCategoryIdAsync(int categoryId)
        {
            var places = await _db.Places
                .Where(p => !string.IsNullOrEmpty(p.Name))
                .ToListAsync();
            places = places.OrderBy(p => p.Name).ToList();
            return places.Where(p => p.CategoryId == categoryId).Select(_mapper.Map<Domain.PlaceAggregate.Place>).ToList();
        }

        public async Task<List<Domain.PlaceAggregate.Place>> GetPlacesByIdsAsync(List<Guid> placeIds)
        {
            var places = await _db.Places
                    .ToListAsync();
            places = places.OrderBy(p => p.Name).ToList();
            return places
                .Where(p => placeIds.Contains(p.Id))
                .Where(p => !string.IsNullOrEmpty(p.Name))
                .Select(_mapper.Map<Domain.PlaceAggregate.Place>).ToList();
        }
    }
}