using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Institute
{
    public class EntityFrameworkInstituteRepository : IInstituteRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public EntityFrameworkInstituteRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Domain.InstituteAggregate.Institute> GetAsync(IFilterSpecification<Domain.InstituteAggregate.Institute, IInstituteSpecificationVisitor> specification)
        {
            var instituteDb = await _db.Institutes.SingleOrDefaultAsync(specification.ToFilterExpression<Domain.InstituteAggregate.Institute, ExpressionInstituteSpecificationVisitor, IInstituteSpecificationVisitor, InstituteDb>());

            return _mapper.Map<Domain.InstituteAggregate.Institute>(instituteDb);
        }
    }
}
