using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter
{
    /// <summary>
	/// Defines application specific initialization configuration and settings
	/// </summary>
	public class Startup : IGreenShelterApplication
    {
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "Startup"; } }
				
        /// <summary>
		/// Constructor
		/// </summary>
		public Startup()
        {
			this.WriteInformation(TagName);
		}

        /// <summary>
		/// 
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
        {
			this.WriteInformation("\tConfigure Services");
			
            // Add MVC services to the services container
            services.AddMvc();
        }

        /// <summary>
		/// This method is invoked when KRE_ENV is 'Development' or is not defined
        /// The allowed values are Development,Staging and Production
		/// </summary>
		public void ConfigureDevelopment(IApplicationBuilder app)
        {
			this.WriteInformation("\tConfigure Development");
			
            //Display custom error page in production when error occurs
            //During development use the ErrorPage middleware to display error information in the browser
            app.UseErrorPage(ErrorPageOptions.ShowAll);

            // Add the runtime information page that can be used by developers
            // to see what packages are used by the application
            // default path is: /runtimeinfo
            app.UseRuntimeInfoPage();

            Configure(app);
        }

        /// <summary>
		/// This method is invoked when KRE_ENV is 'Staging'
        /// The allowed values are Development,Staging and Production        
		/// </summary>
		public void ConfigureStaging(IApplicationBuilder app)
        {
			this.WriteInformation("\tConfigure Staging");
			
            app.UseErrorHandler("/Home/Error");
            Configure(app);
        }

        /// <summary>
		/// This method is invoked when KRE_ENV is 'Production'
        /// The allowed values are Development,Staging and Production        
		/// </summary>
		public void ConfigureProduction(IApplicationBuilder app)
        {
			this.WriteInformation("\tConfigure Production");
			
            app.UseErrorHandler("/Home/Error");
            Configure(app);
        }

        /// <summary>
		/// 
		/// </summary>
		public void Configure(IApplicationBuilder app)
        {
			this.WriteInformation("\tConfigure");
			
            // Add static files to the request pipeline
            app.UseStaticFiles();

            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "areaRoute",
                //    template: "{area:exists}/{controller}/{action}",
                //    defaults: new { action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });

                //routes.MapRoute(
                //    name: "api",
                //    template: "{controller}/{id?}");
            });
        }
    } // end class
} // end namespace