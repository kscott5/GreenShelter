using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Security;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.AspNet.Security.Google;
using Microsoft.AspNet.Security.Facebook;
using Microsoft.AspNet.Security.MicrosoftAccount;
using Microsoft.AspNet.Security.Twitter;
using Microsoft.AspNet.StaticFiles;

using Microsoft.Framework.OptionsModel;

using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter {
	public partial class Startup {
		/// <summary>
		/// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		/// </summary>
        public void ConfigureAuthenticiation(IApplicationBuilder app) {
			this.WriteInformation("\tConfigure Authenticiation");

			StaticFileOptions staticFileOptions = new StaticFileOptions();
			((FileExtensionContentTypeProvider)staticFileOptions.ContentTypeProvider).Mappings.Add(".woff2", "application/font-woff2");
			
            // Add static files to the request pipeline
            app.UseStaticFiles(staticFileOptions);
            app.UseIdentity();	

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			// Configure the sign in cookie
			//app.UseCookieAuthentication();
			
			if(this.GoogleEnabled()) {
				app.UseGoogleAuthentication();
			}
        }
    } // end class
} // end namespace