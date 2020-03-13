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

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Category
{
    public class EntityFrameworkCategoryRepository
    {
        public EntityFrameworkCategoryRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public async Task<List<Domain.CategoryAggregate.Category>> GetAllCategoryAsync()
        {
            var categories = await _db.Categories.Include(p => p).ToListAsync();
            return categories.Select(_mapper.Map<Domain.CategoryAggregate.Category>).ToList();
        }
    }
}
