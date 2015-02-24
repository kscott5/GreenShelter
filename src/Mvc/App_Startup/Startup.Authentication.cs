using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Security;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.AspNet.Security.Google;
using Microsoft.AspNet.StaticFiles;

using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;

using PCSC.GreenShelter.Extensions;

using System;

namespace PCSC.GreenShelter {
	public partial class Startup {
		/// <summary>
		/// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		/// </summary>
        public void ConfigureAuthenticiation(IApplicationBuilder app) {
			this.WriteInformation("\tConfigure Authentication");

		   	// Retrieve the Options Manager for CoookieAuthenticationOptions
			var cookieOptionManager = app.ApplicationServices
				.GetService(typeof(IOptions<CookieAuthenticationOptions>)) as OptionsManager<CookieAuthenticationOptions>;
			
			// Retrieve the Options for Application Cookie Authentication 
			var cookieAuthOptions = cookieOptionManager.GetNamedOptions(IdentityOptions.ApplicationCookieAuthenticationType) as CookieAuthenticationOptions;
						
			// Update the Cookie Authentication Options LoginPath and LogoutPath
			cookieAuthOptions.LoginPath = new PathString("/#client/login");
			cookieAuthOptions.LogoutPath = new PathString("/#client/logout");

			// Add mappings to static files
			// This should be include in the master branch
			var staticFileOptions = new StaticFileOptions();
			((FileExtensionContentTypeProvider)staticFileOptions.ContentTypeProvider).Mappings.Add(".woff2", "application/font-woff2");
			((FileExtensionContentTypeProvider)staticFileOptions.ContentTypeProvider).Mappings.Add(".json", "application/json");
			
            // Add static files to the request pipeline
            app.UseStaticFiles(staticFileOptions);
           			
			// Add the Identity related middleware to the request pipeline
			app.UseIdentity();
			
			// Add Google as external authentication to request pipeline
			app.UseGoogleAuthentication();
        }
    } // end class
} // end namespace