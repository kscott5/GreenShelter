using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.AspNet.Security.Google;
using Microsoft.AspNet.Security.Facebook;
using Microsoft.AspNet.Security.MicrosoftAccount;
using Microsoft.AspNet.Security.Twitter;

using PCSC.GreenShelter.Extensions;


namespace PCSC.GreenShelter {
	public partial class Startup {
		/// <summary>
		/// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		/// </summary>
        public void ConfigureAuthenticiation(IApplicationBuilder app) {
			this.WriteInformation("\tConfigure Authenticiation");

			// Enable the application to use a cookie to store information for the signed in user
			// and to use a cookie to temporarily store information about a user logging in with a third party login provider
			// Configure the sign in cookie
			app.UseCookieAuthentication();

			if(this.MicrosoftAccountEnabled()) {
				app.UseMicrosoftAccountAuthentication();
			}

			if(this.TwitterEnabled()) {
				app.UseTwitterAuthentication();
			}

			if(this.FacebookEnabled()) {
				app.UseFacebookAuthentication();
			}

			if(this.GoogleEnabled()) {
				app.UseGoogleAuthentication();
			}
        }
    } // end class
} // end namespace