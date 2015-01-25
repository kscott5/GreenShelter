using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Routing;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;
using PCSC.GreenShelter.Data;

namespace PCSC.GreenShelter
{
    /// <summary>
	/// Defines application specific initialization configuration and settings
	/// </summary>
	public partial class Startup : IGreenShelterApplication
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
		/// Configure middleware services
		/// </summary>
		public void ConfigureServices(IServiceCollection services)
        {
			this.WriteInformation("\tConfigure Services");
			
            // Add MVC services to the services container
            services.AddMvc();
			
			Func<IServiceProvider, ApplicationContext> contextFactory = _ => GreenShelterData.GetContext();
			services.AddSingleton(contextFactory);
			
			Func<IServiceProvider, ApplicationUserManager> userManagerFactory = _ => GreenShelterData.GetUserManager();
			services.AddSingleton(userManagerFactory);
			
			Func<IServiceProvider, ApplicationRoleManager> roleManagerFactory = _ => GreenShelterData.GetRoleManager();
			services.AddSingleton(roleManagerFactory);
        }

        /// <summary>
		/// 
		/// </summary>
		public void Configure(IApplicationBuilder app)
        {
			this.WriteInformation("\tConfigure");
			
			this.ConfigureAuth(app);
			 
            // Add static files to the request pipeline
            app.UseStaticFiles();

            // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    } // end class
} // end namespace