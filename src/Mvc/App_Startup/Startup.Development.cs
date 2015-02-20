using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Routing;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    /// <summary>
	/// Defines application specific initialization configuration and settings
	/// </summary>
	public partial class Startup {
        /// <summary>
		/// This method is invoked when KRE_ENV is 'Development' or is not defined
        /// The allowed values are Development,Staging and Production
		/// </summary>
		public void ConfigureDevelopment(IApplicationBuilder app,  ILoggerFactory loggerFactory)
        {
			this.WriteInformation("\tConfigure Development");
			//loggerFactory.AddConsole();
			
            //Display custom error page in production when error occurs
            //During development use the ErrorPage middleware to display error information in the browser
            app.UseErrorPage(ErrorPageOptions.ShowAll);

            // Add the runtime information page that can be used by developers
            // to see what packages are used by the application
            // default path is: /runtimeinfo
            app.UseRuntimeInfoPage();

            Configure(app);
        }
    } // end class
} // end namespace