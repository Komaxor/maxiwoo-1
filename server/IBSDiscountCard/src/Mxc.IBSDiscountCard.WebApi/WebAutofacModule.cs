using Autofac;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Common.Metrics;
using Mxc.IBSDiscountCard.WebApi.Metrics;
using Mxc.WebApi.Abstractions.HttpExceptionProvider;
using Mxc.Helpers.DateTime;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Infrastructure.LoggedInUserAccessor;

namespace Mxc.IBSDiscountCard.WebApi
{
    /// <summary>
    /// Autofac config for the WebApi project
    /// </summary>
    public class WebAutofacModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<HttpExceptionProvider<FunctionCodes>>().As<IHttpExceptionProvider>().SingleInstance();
            builder.RegisterType<UtcDateTimeHelper>().As<IDateTimeHelper>().SingleInstance();
            builder.RegisterType<MetricsService>().As<IMetricsService>().SingleInstance();
            builder.RegisterType<HttpLoggedInUserAccessor>().As<ILoggedInUserAccessor>().SingleInstance();
        }
    }
}