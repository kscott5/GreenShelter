using System;
using System.Security.Claims;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Security;
using Microsoft.AspNet.Security.Cookies;
using Microsoft.AspNet.StaticFiles;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.SqlServer;

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

			// Configure options for Identity Manager
			services.Configure<IdentityOptions>(options => {
				options.User = new UserOptions { 
					RequireUniqueEmail = true
				};
			
				options.Password = new PasswordOptions { 
					RequiredLength = 6, 
					RequireNonLetterOrDigit = true, 
					RequireDigit = true, 
					RequireLowercase = true,
					RequireUppercase = true
				};

				options.ClaimsIdentity = new ClaimsIdentityOptions {
					RoleClaimType = ClaimTypes.Role,
					UserNameClaimType = ClaimTypes.Name,
					UserIdClaimType = ClaimTypes.NameIdentifier
				};
            });
			
			// Configure options for cookie authentication
			services.Configure<CookieAuthenticationOptions>(options => {
				options.AuthenticationType = IdentityOptions.ApplicationCookieAuthenticationType;
				options.AuthenticationMode = AuthenticationMode.Active;
				options.CookieName = IdentityOptions.ApplicationCookieAuthenticationType;

				options.LoginPath = new PathString("/#client/login");
				options.LogoutPath = new PathString("/#client/logout");
			});

			services.ConfigureCookieAuthentication(options => {
				options.AuthenticationType = IdentityOptions.ApplicationCookieAuthenticationType;
				options.AuthenticationMode = AuthenticationMode.Active;
				options.CookieName = IdentityOptions.ApplicationCookieAuthenticationType;

				options.LoginPath = new PathString("/#client/login");
				options.LogoutPath = new PathString("/#client/logout");
			});
			
			if(this.GoogleEnabled()) {
				services.ConfigureGoogleAuthentication(options => {
					options.ClientId = this.GoogleClientId();
					options.ClientSecret = this.GoogleClientSecret();
					options.CallbackPath = new PathString("/api/v1/client/externallogincallback");
					options.Scope.Add("profile");
					options.Scope.Add("email");
				});
			}

			services.AddScoped(typeof(DataProtectorTokenProvider<ApplicationUser>));
			services.AddScoped(typeof(ApplicationUserManager));
			services.AddScoped(typeof(ApplicationRoleManager));
			
			// Remember you need to cast to concrete implements of IClaimsIdentityFactory
			// For example:
			//
			//		var factory = serviceProvider.
			//			GetRequiredService<IClaimsIdentityFactory<ApplicationUser>>() 
			//				as ApplicationClaimsIdentityFactory;
			services.AddScoped(typeof(IClaimsIdentityFactory<ApplicationUser>), typeof(ApplicationClaimsIdentityFactory));
			
			// Add SQL Server with EF service to the service container
			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<GreenShelterDbContext>(options => {
					options.UseSqlServer(this.ConnectionString());
				});
			
			
			// Add Identity services to the services container
            services.AddIdentity<ApplicationUser, ApplicationRole>(this.Configuration())
				.AddEntityFrameworkStores<GreenShelterDbContext, int>()
				//.AddTokenProvider(typeof(DataProtectorTokenProvider<ApplicationUser>));
				.AddDefaultTokenProviders();
				
			// Add MVC services to the services container
            services.AddMvc();			
		}
    } // end class
} // end namespace