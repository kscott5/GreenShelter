using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Security.Cookies;

using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;
using PCSC.GreenShelter.Data;

namespace PCSC.GreenShelter {
	public partial class Startup {
		/// <summary>
		/// Configures the cookie authentication options received received from IApplicationBuilder.UseCookieAuthentication() method
		/// </summary>
		public void ConfigureCookieAuthenticationOptions(CookieAuthenticationOptions options) {
			options.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
			options.LoginPath = new PathString("/Site/Login");
			options.LogoutPath = new PathString("/Site/Logout");
		}

		/// <summary>
		/// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		/// </summary>
        public void ConfigureAuth(IApplicationBuilder app) {
			this.WriteInformation("\tConfigureAuth");
			
			Action<CookieAuthenticationOptions> configOption = ConfigureCookieAuthenticationOptions;
			
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            //app.UseCookieAuthentication(configOption);
			
            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
    } // end class
} // end namespace