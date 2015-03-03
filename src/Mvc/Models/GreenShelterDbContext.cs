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
using Microsoft.Framework.Logging;

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
		
 		public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider) {
			AppUtilityHelper.Logger.WriteInformation("Initializing the database asynchronously");
			
            using (var db = serviceProvider.GetRequiredService<GreenShelterDbContext>()) {
				var sqlServerDatabase = db.Database.AsSqlServer();
				
                if (sqlServerDatabase != null) {
					AppUtilityHelper.Logger.WriteInformation("Sql Server Database ensured created");
					
					await CreateRoles(serviceProvider);
                    await CreateAdminUser(serviceProvider);
                }
            } // end using
        } // end InitializeDatabaseAsync		
		
		/// <summary>
        /// Creates the store manager roles for the application
        /// </summary>
        /// <param name="serviceProvider"></param>
		/// <param name="application"></param>
        /// <returns></returns>
        private static async Task CreateRoles(IServiceProvider serviceProvider) {
			AppUtilityHelper.Logger.WriteInformation("Creating the Application Roles asynchronously");
			
			/*
				QUESTION: Is this better than registering the options and then calling its action
				SOLUTION: Time the execution
			*/
			var options = new IdentityOptions();
			OptionsServices.ReadProperties(options, AppUtilityHelper.Configuration);
			
			var claimType = options.ClaimsIdentity.RoleClaimType;
			
            var roleManager = serviceProvider.GetService<ApplicationRoleManager>();
			var role = await roleManager.FindByNameAsync(ApplicationRole.Administrator);			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = ApplicationRole.Administrator, Description = "Person for overall site maintenance and usability"});
				role = await roleManager.FindByNameAsync(ApplicationRole.Administrator);
				await roleManager.AddClaimAsync(role, new Claim(claimType, role.Name));
			}
			
			role = await roleManager.FindByNameAsync(ApplicationRole.Organization);			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = ApplicationRole.Organization, Description = "A business entity responsible for providing services to a client"});
				role = await roleManager.FindByNameAsync(ApplicationRole.Organization);
				await roleManager.AddClaimAsync(role, new Claim(claimType, role.Name));
			}
			
			role = await roleManager.FindByNameAsync(ApplicationRole.Volunteer);			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = ApplicationRole.Volunteer, Description = "A person who serves the organization and clients without payment"});
				role = await roleManager.FindByNameAsync(ApplicationRole.Volunteer);
				await roleManager.AddClaimAsync(role, new Claim(claimType, role.Name));
			}
			
			role = await roleManager.FindByNameAsync(ApplicationRole.Client);			
			if(role == null) {
				await roleManager.CreateAsync(new ApplicationRole{Name = ApplicationRole.Client, Description = "Person receiving services from an organization"});		
				role = await roleManager.FindByNameAsync(ApplicationRole.Client);
				await roleManager.AddClaimAsync(role, new Claim(claimType, role.Name));
			}
		}
		
		/// <summary>
        /// Creates a store manager user who can manage the inventory.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        private static async Task CreateAdminUser(IServiceProvider serviceProvider) {
			AppUtilityHelper.Logger.WriteInformation("Creating the Administrator User asynchronously");
			
			var userManager = serviceProvider.GetService<ApplicationUserManager>();
			
			var user = await userManager.FindByNameAsync(AppUtilityHelper.Configuration.DefaultAdminUserName());
            if (user == null) {
				user = new ApplicationUser { UserName = AppUtilityHelper.Configuration.DefaultAdminUserName(), Email = AppUtilityHelper.Configuration.DefaultAdminUserName() };
				var result = await userManager.CreateAdminAsync(user, AppUtilityHelper.Configuration.DefaultAdminPassword());
				
				if(result != IdentityResult.Success) {
					throw new Exception("Failed to created System Administrator User");
				}
			}
        } 
	} // end class
} // end namespace