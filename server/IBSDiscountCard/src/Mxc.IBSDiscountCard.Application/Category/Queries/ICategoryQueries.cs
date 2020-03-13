using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mxc.Domain.Abstractions.Repositories;
namespace Mxc.IBSDiscountCard.Application.Category.Queries
{
    public interface ICategoryQueries
    {
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
    }
}
