using AutoMapper;
using Mxc.Domain.Abstractions.Repositories;
using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Microsoft.Extensions.Logging;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.User
{
    public class EntityFrameworkUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly ILogger<EntityFrameworkUserRepository> _logger;

        public EntityFrameworkUserRepository(ApplicationDbContext db, ILogger<EntityFrameworkUserRepository> logger, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Domain.UserAggregate.User> GetAsync(IFilterSpecification<Domain.UserAggregate.User, IUserSpecificationVisitor> specification)
        {
            var userDb = await GetBaseQuery().SingleOrDefaultAsync(specification.ToFilterExpression<Domain.UserAggregate.User, ExpressionUserSpecificationVisitor, IUserSpecificationVisitor, UserDb>());

            if (userDb == null)
            {
                return null;
            }

            return new Domain.UserAggregate.User(new UserId(userDb.Id),
                userDb.FullName,
                userDb.Email,
                userDb.PasswordHash,
                _mapper.Map<Subscription>(userDb.Subscription),
                userDb.ProfilePhoto,
                new InstituteId(userDb.InstitudeId),
                userDb.ActivationCode,
                userDb.EmailConfirmed,
                userDb.CustomerId);
        }

        public async Task<bool> UpdateAsync(Domain.UserAggregate.User item, IFilterSpecification<Domain.UserAggregate.User, IUserSpecificationVisitor> guardSpecification = null)
        {
            var query = GetBaseQuery();

            if (guardSpecification != null)
            {
                query = query.Where(guardSpecification.ToFilterExpression<Domain.UserAggregate.User, ExpressionUserSpecificationVisitor, IUserSpecificationVisitor, UserDb>());
            }

            var db = await query.SingleOrDefaultAsync(a => a.Id == item.Id.Id);

            _mapper.Map(item, db);

            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException e)
            {
                _logger.LogError(e, "User updated not fullfiled in user repo.");
                return false;
            }
        }

        private IQueryable<UserDb> GetBaseQuery()
        {
            return _db.Users.Include(a => a.Subscription);
        }
    }
}
