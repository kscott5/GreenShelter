using System;

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Identity;

using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

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

			// Add MVC services to the services container
            services.AddMvc();

			services.AddEntityFramework()
				.AddSqlServer()
				.AddDbContext<GreenShelterDbContext>(options => {
					options.UseSqlServer(this.ConnectionString());
				});

			services.Configure<DbContextOptions>(options => {
				Action<SqlServerOptionsExtension> actionExtension = extension => {
					extension.ConnectionString = this.ConnectionString();
				};
	
				((IDbContextOptions)options).AddOrUpdateExtension(actionExtension);
							
			});
			
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
            });
			
			// Add Identity services to the services container
            services.AddIdentity<ApplicationUser, ApplicationRole>(this.Configuration())
				.AddEntityFrameworkStores<GreenShelterDbContext, int>()
				.AddTokenProvider(typeof(DataProtectorTokenProvider<ApplicationUser>));
				
			//.AddMessageProvider<EmailMessageProvider>() /* public class EmailMessageProvider: IIdentityMessageProvider {} */
			//.AddMessageProvider<SmsMessageProvider>(); /* public class SmsMessageProvider: IIdentityMessageProvider {} */
			// Add services needed for application. They are injected into the objects as needed

			services.ConfigureCookieAuthentication(options => {
				options.LoginPath = new PathString("/#client/login");
				options.LogoutPath = new PathString("/#client/logout");
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
					options.Scope.Add("profile");
					options.Scope.Add("email");
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