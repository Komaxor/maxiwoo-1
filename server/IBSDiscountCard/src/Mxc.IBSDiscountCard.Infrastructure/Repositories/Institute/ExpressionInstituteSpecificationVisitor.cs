using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Institute
{
    public class ExpressionInstituteSpecificationVisitor : ExpressionVisitor<InstituteDb, ExpressionInstituteSpecificationVisitor, IInstituteSpecificationVisitor,
        Domain.InstituteAggregate.Institute>, IInstituteSpecificationVisitor
    {
        public void Visit(IdEquals spec)
        {
            FilterExpression = db => db.Id == spec.InstituteId.Id;
        }
    }
}
