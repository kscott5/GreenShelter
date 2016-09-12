using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Microsoft.AspNet.Antiforgery;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Abstractions;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Session;

using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Primitives;

using Autofac;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    public class Startup : IGreenShelterApplication
    {
        private IConfigurationRoot configuration;
        
        public string TagName { get {return "Startup"; } }
        
        public Startup(IHostingEnvironment env) {
            System.Console.WriteLine(new ConfigurationBuilder().GetBasePath());
            
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            configuration = builder.Build();           
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure session to use built-in in-memory caching option 
            // TODO: Configure session to using IDistributedCache (not built-in memory caching option)
            services.AddCaching();

            // Configure Session Options
            var sessionOptionsConfig = configuration.GetSection("SessionOptions");
            services.Configure<SessionOptions>(sessionOptionsConfig);
            
            // Add Session to services container
            services.AddSession();
            
            var connectionString = configuration["Data:DefaultConnectionString:ConnectionString"];

            // Configure Identity Options
            var identityOptionsConfig = configuration.GetSection("IdentityOptions");
            services.Configure<IdentityOptions>(identityOptionsConfig);            
            
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<GreenShelterDbContext>(option => 
                    option.UseSqlite(connectionString));
            
            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<GreenShelterDbContext,int>()
                .AddDefaultTokenProviders();

            // Add MVC services to the services container.
            services.AddMvc();
            
            // Configure Identity Options
            var antiforgeryOptionsConfig = configuration.GetSection("AntiforgeryOptions");
            services.Configure<AntiforgeryOptions>(antiforgeryOptionsConfig);            

            // Add Antiforgery services to the service container
            services.AddAntiforgery();
            
            // HACK: Header based Antiforgery Token won't be supported until RC2 
            var serviceDescriptor = ServiceDescriptor.Singleton(typeof(IAntiforgeryTokenStore), typeof(CustomAntiforgeryTokenStore));
            services.Replace(serviceDescriptor);            
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {            
            // Configure application wide logging
            var loggingConfiguration = configuration.GetSection("Logging");
            var loggerSettings = new ConfigurationConsoleLoggerSettings(loggingConfiguration);
                       
            loggerFactory.AddConsole(loggerSettings);
            loggerFactory.AddDebug((string name, LogLevel level) => { 
                LogLevel result;
                return loggerSettings.TryGetSwitch(name, out result) && level >= level;
            });
         
            // Configure the HTTP request pipeline.

            app.UseSession();
            
            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseRuntimeInfoPage();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage((DatabaseErrorPageOptions options) => options = new DatabaseErrorPageOptions { 
                    ShowExceptionDetails = true,
                    ListMigrations = true,
                    EnableMigrationCommands = true,
                    MigrationsEndPointPath = new PathString("/Migrations")
                });
            }
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
                app.UseExceptionHandler("/Spa/Error");
            }

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Spa}/{action=StartPage}/{id?}");
            });
            
            app.ApplicationServices.EnsureDbContextCreatedAndSeeded();
        }
        
        #region Entity point for the application
        public static void Main(string[] args)
        {
            WebApplication.Run<Startup>();
        }
        #endregion

    }
}
