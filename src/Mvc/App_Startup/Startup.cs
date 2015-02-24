using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Routing;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter {
	/// <summary>
	/// Defines application specific initialization configuration and settings
	/// </summary>
	public partial class Startup : IGreenShelterApplication {
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "Startup"; } }

		/// <summary>
		/// Constructor
		/// </summary>
		public Startup() {
		}

        /// <summary>
		/// 
		/// </summary>
		public void Configure(IApplicationBuilder app) {
			this.WriteInformation("\tConfigure");
			
			app.UseErrorPage(ErrorPageOptions.ShowAll);
						
			this.ConfigureAuthenticiation(app);
			 
			 // Add MVC to the request pipeline
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Spa", action = "StartPage" });
            });

			GreenShelterDbContext.InitializeDatabaseAsync(app.ApplicationServices, this).Wait();
        }
    } // end class
} // end namespace