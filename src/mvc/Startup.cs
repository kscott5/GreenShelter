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
using Microsoft.Extensions.Logging.TraceSource;
using Microsoft.Extensions.PlatformAbstractions;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    public class Startup : IGreenShelterApplication
    {
        private IConfiguration configuration;
        private ILogger logger;
        
        public string TagName { get {return "Startup"; } }
        
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv, ILoggerFactory loggerFactory)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            this.configuration = builder.Build();
            
            var sourceSwitch = new SourceSwitch("GreenShelter") { Level = SourceLevels.All };          
            loggerFactory.AddTraceSource(sourceSwitch, new ConsoleTraceListener());
            loggerFactory.AddTraceSource(sourceSwitch, new TextWriterTraceListener("./GreenShelter.log"));

            this.logger = loggerFactory.CreateLogger(this.TagName);        
            this.logger.LogInformation(".ctor: Environment->{0}", env.EnvironmentName);
            
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.logger.LogInformation("ConfigureServices");
            
            var connectionString = this.configuration["Data:DefaultConnection:ConnectionString"];
            this.logger.LogDebug("Connection String: {0}", connectionString);
            
            services.AddInstance(this.configuration);
            
                        
            // Add Identity services to the services container.
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<GreenShelterDbContext,int>()
                .AddDefaultTokenProviders();

            // Add MVC services to the services container.
            services.AddMvc();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            //services.AddWebApiConventions();

            // Register application services.
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            this.logger.LogInformation("Configure");
            
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

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                routes.MapWebApiRoute("DefaultApi", "api/v1/{controller}/{id?}");
            });
        }
    }
}
