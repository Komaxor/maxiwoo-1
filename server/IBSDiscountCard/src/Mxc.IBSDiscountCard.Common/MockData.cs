using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mxc.IBSDiscountCard.Common
{
    public static class MockData
    {
        public static Guid InstitudeId { get => Guid.Parse("e5906014-cc56-4aff-abb3-2db3cdb40a21"); }
        public static string InstituteName { get => "International Business School"; }
    }
}
