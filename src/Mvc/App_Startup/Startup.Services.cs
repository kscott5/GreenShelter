using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Routing;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
	/// <summary>
	/// Defines application specific initialization configuration and settings
	/// </summary>
	public partial class Startup {
		/// <summary>
		/// Configure middleware services
		/// </summary>
		public void ConfigureServices(IServiceCollection services) {
			this.WriteInformation("\tConfigure Services");

			// Add MVC services to the services container
            services.AddMvc();

			services.AddEntityFramework(this.Configuration())
                        .AddSqlServer()
                        .AddDbContext<GreenShelterDbContext>();

			// Add Identity services to the services container
            services.AddIdentity<ApplicationUser, ApplicationRole>(this.Configuration())
                    .AddEntityFrameworkStores<GreenShelterDbContext>()
                    .AddDefaultTokenProviders();

			//.AddMessageProvider<EmailMessageProvider>() /* public class EmailMessageProvider: IIdentityMessageProvider {} */
			//.AddMessageProvider<SmsMessageProvider>(); /* public class SmsMessageProvider: IIdentityMessageProvider {} */

			services.ConfigureCookieAuthentication(options => {
				options.LoginPath = new PathString("/Site/Login");
				options.LogoutPath = new PathString("/Site/Logout");
			});
			
			if(this.FacebookEnabled()) {
				services.ConfigureFacebookAuthentication(options => {
					options.AppId = this.FacebookAppId();
					options.AppSecret = this.FacebookAppSecret();
				});
			}

			if(this.GoogleEnabled()) {
				services.ConfigureGoogleAuthentication(options => {
					options.ClientId = this.GoogleClientId();
					options.ClientSecret = this.GoogleClientSecret();
				});
			}

			if(this.MicrosoftAccountEnabled()) {
				services.ConfigureMicrosoftAccountAuthentication(options => {
					options.ClientId = this.MicrosoftAccountClientId();
					options.ClientSecret = this.MicrosoftAccountClientSecret();
				});
			}

			if(this.TwitterEnabled()) {
				services.ConfigureTwitterAuthentication(options => {
					options.ConsumerKey = this.TwitterConsumerKey();
					options.ConsumerSecret = this.TwitterConsumerSecret();
				});
			}

        }
    } // end class
} // end namespace