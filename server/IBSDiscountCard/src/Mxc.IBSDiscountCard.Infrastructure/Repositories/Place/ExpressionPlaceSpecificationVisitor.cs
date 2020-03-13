using Mxc.Domain.Abstractions.Specifications;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate.Specifications;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place.Models;
using System;
using System.Linq;

namespace Mxc.IBSDiscountCard.Infrastructure.Repositories.Place
{
    public class ExpressionPlaceSpecificationVisitor
        : ExpressionVisitor<PlaceDb, ExpressionPlaceSpecificationVisitor, IPlaceSpecificationVisitor,
            Domain.PlaceAggregate.Place>, IPlaceSpecificationVisitor
    {
        public void Visit(IdEquals spec)
        {
            FilterExpression = db => db.Id == spec.PlaceId.Id;
        }

        public void Visit(PlaceNameContains spec)
        {
            FilterExpression = db => db.Name.ToLower().Contains(spec.Name.ToLower().Trim());
        }

        public void Visit(PlaceTypeContains spec)
        {
            FilterExpression = db => !string.IsNullOrWhiteSpace(db.Type) && db.Type.ToLower().Contains(spec.Type.ToLower().Trim());
        }

        public void Visit(PlaceTagsContains spec)
        {
            FilterExpression = db => db.Tags != null && db.Tags.Contains(spec.Tag);
        }

        public void Visit(SortByPlaceName spec)
        {
            SorterExpression = db => db.Name;
        }

        public void Visit(NotHidden spec)
        {
            FilterExpression = db => !db.IsHidden;
        }
    }
}