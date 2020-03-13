using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Mxc.IBSDiscountCard.Application.Category.Queries;
using System.Threading.Tasks;
using Mxc.IBSDiscountCard.Infrastructure.Repositories;
using System.Linq;

namespace Mxc.IBSDiscountCard.WebApi.Controllers
{
    public class CategoryController: ApiControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(_db.Categories.ToList());
        }
    }
}
