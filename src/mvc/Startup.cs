using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    public class Startup : IGreenShelterApplication
    {
        private IConfigurationRoot configuration;
        private ILogger logger;
        
        public string TagName { get {return "Startup"; } }
        
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            this.logger = loggerFactory.CreateLogger(this.TagName);        
            this.logger.LogDebug(".ctor: Environment->{0}", env.EnvironmentName);
            
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            this.configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.logger.LogDebug("ConfigureServices");
            
            services.AddInstance(this.configuration);
            
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
            this.logger.LogDebug("Configure");
            // Configure the HTTP request pipeline.

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Add the following to the request pipeline only in development environment.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
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

            // Add and configure the options for authentication middleware to the request pipeline.
            // You can add options for middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseFacebookAuthentication(options =>
            //{
            //    options.AppId = Configuration["Authentication:Facebook:AppId"];
            //    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});
            //app.UseGoogleAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});
            //app.UseMicrosoftAccountAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:MicrosoftAccount:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:MicrosoftAccount:ClientSecret"];
            //});
            //app.UseTwitterAuthentication(options =>
            //{
            //    options.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //    options.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //});

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