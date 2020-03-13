using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace Mxc.IBSDiscountCard.Infrastructure
{
    public class DevInfrastructureAutofacModule : InfrastructureAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
