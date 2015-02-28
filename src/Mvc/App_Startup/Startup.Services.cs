using System;
using System.Security.Claims;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
			
			/**********************************************************
			NOTE: 	The order in which you register the service and the
					options used by that services is important. 
					
					For instance, the following method will register the 
					Mvc related services registers the MvcOptionSetup 
					which contains the AntiForgeryOptions.

					Once the AddMvc methods finishes, you can then create 
					a new AntiForgeryOptions and change the CookieName and 
					FormFieldName.
					
					The same holds true for any service such as Identity 
					and AddIdentity.
					
					Once the AddIdentity method finishes, you can then
					create a new CookieAuthenticationOptions for
					ApplicationCookieAuthenticationType and change the
					LoginPath and LogoutPath.

					Review the Startup.cs and Startup_Authentication.cs
					for examples
			**********************************************************/			
			// Register MVC with the application services
            services.AddMvc();			
						
			// Register the required EF objects with the application services
			services.AddEntityFramework()
				.AddDbContext<GreenShelterDbContext>(options => { options.UseSqlServer(this.ConnectionString()); })
				.AddSqlServer();

			// Register the required Identity objects with the application services
			services.AddIdentity<ApplicationUser,ApplicationRole>(AppUtilityHelper.Configuration)
				.AddEntityFrameworkStores<GreenShelterDbContext, int>();
							
			// Register the GoogleAuthenticationOptions with the application services
			services.ConfigureGoogleAuthentication();
							
			// Register the authorization objects with the application services
			//services.AddAuthorization(AppUtilityHelper.Configuration);
			
			services.AddScoped(typeof(IUserTokenProvider<>), typeof(DataProtectorTokenProvider<>));
			services.AddScoped(typeof(ApplicationUserManager), typeof(ApplicationUserManager));
			services.AddScoped(typeof(ApplicationUserStore), typeof(ApplicationUserStore));
			services.AddScoped(typeof(ApplicationRoleManager));
			services.AddScoped(typeof(ApplicationRoleStore));
			services.AddScoped(typeof(ApplicationSignInManager), typeof(ApplicationSignInManager));
			
		}
    } // end class
} // end namespace