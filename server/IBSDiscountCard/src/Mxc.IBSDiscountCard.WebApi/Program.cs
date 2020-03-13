#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mxc.IBSDiscountCard.Infrastructure.Repositories;
using Mxc.WebApi.Abstractions.Logging;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Mxc.IBSDiscountCard.Application.Image.Services;

namespace Mxc.IBSDiscountCard.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogConfigurator.ConfigureSerilog(args);

            try
            {
                Log.Information("Starting web host");

                Log.Information("Environment: {Env}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    var hostingEnv = serviceProvider.GetService<IHostingEnvironment>();
                    var userDbContext = serviceProvider.GetService<ApplicationDbContext>();
                    var fileProvider = serviceProvider.GetService<IFileProvider>();

                    if (!fileProvider.CheckStorage())
                    {
                        Log.Error("Storage not exists for storeing image files.");
                        throw new Exception("Storage not exists.");
                    }

                    try
                    {
                        //delete and recreate locally so we don't have to create migrations during local development
                        if (hostingEnv.IsDevelopment())
                        {
                            //userDbContext.Database.EnsureDeleted();
                            userDbContext.Database.EnsureCreated();
                        }
                        //run db migration
                        else
                        {
                            userDbContext.Database.Migrate();
                        }
                    }
                    catch (Exception ex)
                    {
                        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "An error occurred seeding the DB.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .UseIISIntegration()
                .UseSerilog();
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member