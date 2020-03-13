using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;

namespace Mxc.IBSDiscountCard.WebApi
{
    public class DevWebAutoFacModule : WebAutofacModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
//            builder.RegisterType<DebugLoggedInUserAccessor>().As<ILoggedInUserAccessor>().SingleInstance();
        }
    }
}
