using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.OptionsModel;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.SqlServer;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// Provides the the data access for the Green Shelter Application
	/// </summary>
	public class GreenShelterDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, int>, IGreenShelterApplication {
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "GreenShelterDbContext"; } }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Organization> Organizations { get; set; }
		
		protected override void OnConfiguring(DbContextOptions options) {
			this.WriteInformation("Configuring DbContext");
			
			// TODO: How do I configure DbContextOptions with this information
			options.UseSqlServer(this.ConnectionString());
		}
		
 		public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider, IGreenShelterApplication application) {
			application.WriteInformation("Initializing the database asynchronously");
			
            using (var db = serviceProvider.GetRequiredService<GreenShelterDbContext>()) {
				var sqlServerDatabase = db.Database.AsSqlServer();
				
                if (sqlServerDatabase != null && !await sqlServerDatabase.ExistsAsync() && !await sqlServerDatabase.HasTablesAsync()) {
					application.WriteInformation("Sql Server Database ensured created");
					
					await CreateRoles(serviceProvider, application);
                    await CreateAdminUser(serviceProvider, application);
                }
            } // end using
        } // end InitializeDatabaseAsync		
		
		/// <summary>
        /// Creates the store manager roles for the application
        /// </summary>
        /// <param name="serviceProvider"></param>
		/// <param name="application"></param>
        /// <returns></returns>
        private static async Task CreateRoles(IServiceProvider serviceProvider, IGreenShelterApplication application) {
			application.WriteInformation("Creating the Application Roles asynchronously");
			
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();            
			
			var role = await roleManager.FindByNameAsync("Administrator");			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = "Administrator", Description = "Person for overrall site maintenance and usability"});
				role = await roleManager.FindByNameAsync("Administrator");
				await roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, role.Name));
			}
			
			role = await roleManager.FindByNameAsync("Organization");			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = "Organization", Description = "A business entity responsible for providing services to a client"});
				role = await roleManager.FindByNameAsync("Organization");
				await roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, role.Name));
			}
			
			role = await roleManager.FindByNameAsync("Client");			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = "Client", Description = "Person receiving services from an organization"});		
				role = await roleManager.FindByNameAsync("Client");
				await roleManager.AddClaimAsync(role, new Claim(ClaimTypes.Role, role.Name));
			}
		}
		
		/// <summary>
        /// Creates a store manager user who can manage the inventory.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateAdminUser(IServiceProvider serviceProvider, IGreenShelterApplication application) {
			application.WriteInformation("Creating the Administrator User asynchronously");
			
            const string adminRole = "Administrator";

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();            
			var role = await roleManager.FindByNameAsync(adminRole);
			
			if(role == null)
				throw new Exception("Role not created");
		
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var user = await userManager.FindByNameAsync(application.DefaultAdminUserName());
            if (user == null) {
				// TODO: How does EF 7 handle 1-to-1 relationships with Role?
				//       Is this neccessary?
                user = new ApplicationUser { UserName = application.DefaultAdminUserName(), Email = application.DefaultAdminUserName(), Role = role };
                await userManager.CreateAsync(user, application.DefaultAdminPassword());
                await userManager.AddToRoleAsync(user, adminRole); // TODO: Does this address the 1-to-1 with Role?
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, application.DefaultAdminUserName()));
            }
        } 
	} // end class
} // end namespace