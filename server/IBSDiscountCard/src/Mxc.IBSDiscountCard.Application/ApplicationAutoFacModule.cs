using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using Mxc.IBSDiscountCard.Application.Image.Queries;
using Mxc.IBSDiscountCard.Application.Place.Queries;
using Mxc.IBSDiscountCard.Application.User.Queries;
using Module = Autofac.Module;

namespace Mxc.IBSDiscountCard.Application
{
    public class ApplicationAutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PlaceQueries>().As<IPlaceQueries>().InstancePerLifetimeScope();
            builder.RegisterType<ImageQueries>().As<IImageQueries>().InstancePerLifetimeScope();
            builder.RegisterType<UserQueries>().As<IUserQueries>().InstancePerLifetimeScope();

            builder.AddMediatR(ThisAssembly);
        }
    }
}
