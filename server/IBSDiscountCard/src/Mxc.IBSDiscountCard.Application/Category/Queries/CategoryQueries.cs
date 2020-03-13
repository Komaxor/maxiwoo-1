using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Domain.CategoryAggregate;

namespace Mxc.IBSDiscountCard.Application.Category.Queries
{
    public class CategoryQueries : ICategoryQueries
    {
        //private readonly ICategoryRepository _cateogoryRepository;
        //private readonly IMapper _mapper;

        public CategoryQueries()
        {
        }

        Task<List<CategoryViewModel>> ICategoryQueries.GetAllCategoryAsync()
        {
            throw new NotImplementedException();
        }
    }
}
