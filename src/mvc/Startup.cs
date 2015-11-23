using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.PlatformAbstractions;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    public class Startup : IGreenShelterApplication
    {
        private IConfigurationRoot configuration;
        
        public string TagName { get {return "Startup"; } }
        
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            this.configuration = builder.Build();             
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var loggingConfiguration = this.configuration.GetSection("Logging");
            var loggerSettings = new ConfigurationConsoleLoggerSettings(loggingConfiguration);
           
            // NOTE: Wasn't sure if needed
            // Add configuration console logger settings to services container
            services.AddInstance(loggerSettings);
            
            // Create new logger factory
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole(loggerSettings);
            loggerFactory.AddDebug((string name, LogLevel level) => { 
                LogLevel result;
                return loggerSettings.TryGetSwitch(name, out result) && level >= level;
            });
            
            // Add logger factory to services containter
            services.AddInstance(loggerFactory);

            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<GreenShelterDbContext>(options =>
                    options.UseSqlite(this.configuration["Data:DefaultConnection:ConnectionString"]));
            
            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<GreenShelterDbContext,int>()
                .AddDefaultTokenProviders();

            // Add MVC services to the services container.
            services.AddMvc();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Configure the HTTP request pipeline.

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
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
        }
    }
}
