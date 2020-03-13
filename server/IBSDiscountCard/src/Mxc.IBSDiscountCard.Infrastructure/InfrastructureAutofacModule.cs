using Autofac;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mxc.IBSDiscountCard.Application.Image.Services;
using Mxc.IBSDiscountCard.Application.User.Services;
using Mxc.IBSDiscountCard.Common.LoggedInUserAccessor;
using Mxc.IBSDiscountCard.Domain.InstituteAggregate;
using Mxc.IBSDiscountCard.Domain.PlaceAggregate;
using Mxc.IBSDiscountCard.Domain.UserAggregate;
using Mxc.IBSDiscountCard.Infrastructure.Repositories;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Institute;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.Place;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;
using Mxc.IBSDiscountCard.Infrastructure.Services;

namespace Mxc.IBSDiscountCard.Infrastructure
{
    public class InfrastructureAutofacModule : Module, IStartable
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<InfrastructureAutofacModule>().As<IStartable>().SingleInstance();
            builder.RegisterType<PlaceCache>().As<IPlaceCache>().SingleInstance();
            builder.RegisterType<EntityFrameworkPlaceRepository>().As<IPlaceRepository>();
            builder.RegisterType<FileSystemFileProvider>().As<IFileProvider>();
            builder.RegisterType<AspIdentityLoginManager>().As<ILoginManager>();
            builder.RegisterType<EntityFrameworkUserRepository>().As<IUserRepository>();
            builder.RegisterType<EntityFrameworkInstituteRepository>().As<IInstituteRepository>();
            builder.RegisterType<SmtpEmailService>().As<IEmailService>();
            builder.RegisterType<UserNotificationService>().As<IUserNotificationService>();
            builder.RegisterType<BrainTreePaymentGateway>().As<IPaymentGateway>();
        }

        public void Start()
        {
        }
    }
}
