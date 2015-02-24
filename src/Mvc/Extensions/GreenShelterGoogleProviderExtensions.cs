using System;
using System.Linq;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Security.Google;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Extensions {
	public static class GreenShelterGoogleProviderExtensions {
		public static IServiceCollection ConfigureGoogleAuthentication(this IServiceCollection services) {
			var externalConfig = AppUtilityHelper.Configuration.GetSubKey(AuthenticationCredentialOptions.Key);
			
			var credentialOptions = new AuthenticationCredentialOptions();
			OptionsServices.ReadProperties(credentialOptions, externalConfig);
			
			if(credentialOptions.Google.Enabled) {				
				return GoogleAuthenticationExtensions.ConfigureGoogleAuthentication(services, options => {
					options.ClientId = credentialOptions.Google.ClientId;
					options.ClientSecret = credentialOptions.Google.ClientSecret;
					options.CallbackPath = new PathString(credentialOptions.Google.CallbackPath);
					foreach(var scope in credentialOptions.Google.Scope.Split(',')) {
						options.Scope.Add(scope);
					}
				});
			}
			
			return services;
		}

		public static IApplicationBuilder UseGoogleAuthentication(this IApplicationBuilder app) {
			var externalConfig = AppUtilityHelper.Configuration.GetSubKey(AuthenticationCredentialOptions.Key);
	
			var credentialOptions = new AuthenticationCredentialOptions();
			OptionsServices.ReadProperties(credentialOptions, externalConfig);
			
			if(credentialOptions.Google.Enabled) {
				return GoogleAuthenticationExtensions.UseGoogleAuthentication(app);
			}
			
			return app;
		}
	} // end class
} // end namespace