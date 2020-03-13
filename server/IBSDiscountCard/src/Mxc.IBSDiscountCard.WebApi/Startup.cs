using App.Metrics;
using App.Metrics.Extensions.DependencyInjection;
using App.Metrics.Formatters;
using App.Metrics.Formatters.Prometheus;
using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mxc.IBSDiscountCard.Application;
using Mxc.IBSDiscountCard.Infrastructure;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mxc.IBSDiscountCard.Infrastructure.Repositories;
using Mxc.IBSDiscountCard.WebApi.Controllers;
using Mxc.IBSDiscountCard.WebApi.SwaggerExamples;
using Mxc.WebApi.Abstractions.HttpExceptionProvider;
using StartupBase = Mxc.WebApi.Abstractions.StartupBase;
using Mxc.IBSDiscountCard.Common;
using Mxc.IBSDiscountCard.Infrastructure.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using MediatR;
using Mxc.IBSDiscountCard.Application.User.Commands;
using Swashbuckle.AspNetCore.Swagger;

namespace Mxc.IBSDiscountCard.WebApi
{
    public class Startup : StartupBase
    {
        private const string SECRET = "�ber extra top secret sz�veg ami nagyon titkos";

        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment) : base(configuration,
            "ibsdiscountcard")
        {
            Environment = environment;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            var metrics = AppMetrics.CreateDefaultBuilder()
                .OutputMetrics.AsPrometheusPlainText()
                .OutputMetrics.AsPrometheusProtobuf()
                .BuildAndAddTo(services);

            services.AddMetricsEndpoints(options =>
            {
                options.MetricsTextEndpointOutputFormatter =
                    metrics.OutputMetricsFormatters.GetType<MetricsPrometheusTextOutputFormatter>();
                options.MetricsEndpointOutputFormatter =
                    metrics.OutputMetricsFormatters.GetType<MetricsPrometheusProtobufOutputFormatter>();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<UserDb, RoleDb>(c => { c.SignIn.RequireConfirmedEmail = false; })
                .AddRoles<RoleDb>()
                .AddClaimsPrincipalFactory<IbsClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<JWTOption>(o =>
            {
                o.SingKey = SECRET;
                o.ExpireDays = 365;
                o.TokenType = JwtBearerDefaults.AuthenticationScheme;
            });

            var key = Encoding.ASCII.GetBytes(SECRET);

            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.Configure<IdentityOptions>(c =>
                {
                    c.Tokens.AuthenticatorIssuer = "IBS Discount";
                    c.Password.RequireNonAlphanumeric = false;
                    c.Password.RequireDigit = false;
                    c.Password.RequireLowercase = false;
                    c.Password.RequireUppercase = false;
                    c.Password.RequiredUniqueChars = 0;
                    c.Password.RequiredLength = 6;
                }
            );

            services.Configure<InitAdminOptions>(Configuration.GetSection("initAdmin"));
            services.Configure<FileUploadOptions>(Configuration.GetSection("FileUpload"));
            services.Configure<EmailOptions>(Configuration.GetSection("EmailOptions"));
            services.Configure<PolicyOptions>(Configuration.GetSection("PolicyOptions"));
            services.Configure<PaymentOptions>(Configuration.GetSection("Payment"));

            AddDefaultConfiguration(services, typeof(Startup));
        }

        protected override IMvcBuilder AddMvcBuilder(IServiceCollection services)
        {
            return base.AddMvcBuilder(services).AddMetrics();
        }

        protected override void ConfigureFluentValidation(FluentValidationMvcConfiguration fv)
        {
            base.ConfigureFluentValidation(fv);

            fv.RegisterValidatorsFromAssemblyContaining<ApplicationAutoFacModule>();
        }

        protected override void ConfigureAutoMapper(IMapperConfigurationExpression cfg)
        {
            base.ConfigureAutoMapper(cfg);
            cfg.AddProfile<ApplicationMappingProfile>();
            cfg.AddProfile<InfrastructureMappingProfile>();
        }

        public override void ConfigureContainer(ContainerBuilder builder)
        {
            base.ConfigureContainer(builder);

            if (Environment.IsDevelopment())
            {
                builder.RegisterModule<DevWebAutoFacModule>();
            }
            else
            {
                builder.RegisterModule<WebAutofacModule>();
            }

            if (Environment.IsDevelopment())
            {
                builder.RegisterModule<DevInfrastructureAutofacModule>();
            }
            else
            {
                builder.RegisterModule<InfrastructureAutofacModule>();
            }

            builder.RegisterModule<ApplicationAutoFacModule>();
        }

        protected override List<string> CollectDocumentations()
        {
            return new List<string>()
            {
                "api-documentation.xml",
                "application-documentation.xml"
            };
        }

        protected override void ConfigureApiVersioning(ApiVersioningOptions options)
        {
            options.Conventions.Controller<PlaceController>().HasApiVersion(new ApiVersion(1, 0));
            options.Conventions.Controller<ImageController>().HasApiVersion(new ApiVersion(1, 0));
        }

        public override void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            IHttpExceptionProvider exceptionProvider,
            IApiVersionDescriptionProvider provider)
        {
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();
            app.UseAuthentication();

            CreateRolesAsync(app.ApplicationServices).Wait();

            app.ApplicationServices.GetService<IMediator>().Send(new RegisterAdmins()).Wait();

            base.Configure(app, env, exceptionProvider, provider);
        }

        protected override void ConfigreExceptionProvider(IHttpExceptionProvider exceptionProvider)
        {
            base.ConfigreExceptionProvider(exceptionProvider);
            exceptionProvider.Register(FunctionCodes.ImageNotFound, new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.UserLoginRefused,
                new HttpCodeDescription(new StatusCodeResult(401)));
            exceptionProvider.Register(FunctionCodes.GetMyProfileNotFoundUser,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.GetMyProfileNotFoundInstitute,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.SendActivateCodeUserUserNotFound,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.ActivateUserUserNotFound,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.ChangePasswordUserNotFound,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.PasswordResetUserNotFound,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.SubscribeUserNotFound,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.UnsubscribeUserNotFound,
                new HttpCodeDescription(new StatusCodeResult(404)));
            exceptionProvider.Register(FunctionCodes.PasswordResetUpdateNotFullfiled,
                new HttpCodeDescription(new StatusCodeResult(500)));
            exceptionProvider.Register(FunctionCodes.PasswordResetClearUpdateNotFullfiled,
                new HttpCodeDescription(new StatusCodeResult(500)));
        }

        protected override void AddSwagger(IServiceCollection services)
        {
            base.AddSwagger(services);

            services.AddSwaggerGen(c => c.SchemaFilter<AddPlaceCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<LoginUserCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<RegisterUserCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<UploadPlaceImageCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<UploadUserImageCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<ActivateUserCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<ChangeMyPasswordCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<SendPasswordResetCodeCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<SetNewPasswordCommandExample>());
            services.AddSwaggerGen(c => c.SchemaFilter<UnsubscribeCommandExample>());
            services.AddSwaggerGen(c => c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
            {
                In = "header",
                Type = "apiKey",
                Name = "Authorization"
            }));
            services.AddSwaggerGen(c => c.OperationFilter<AuthorizationSwaggerOperationFilter>());
        }

        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<RoleDb>>();
            string[] roles = {Roles.Admin, Roles.Customer};

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new RoleDb(role));
                }
            }
        }
    }
}