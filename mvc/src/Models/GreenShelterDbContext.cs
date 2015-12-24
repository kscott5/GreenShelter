using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

using PCSC.GreenShelter;

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
		private ILogger<GreenShelterDbContext> logger;
		
		public GreenShelterDbContext(ILogger<GreenShelterDbContext> logger) {
			this.logger = logger;
		}
		
		protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            this.logger.LogInformation("onConfiguring");
            
			base.OnConfiguring(builder);
		}
        
        protected override void OnModelCreating(ModelBuilder builder) {
            this.logger.LogInformation("onConfiguring");
            base.OnModelCreating(builder);            
        }
        
        public void EnsureCreatedAndSeeded(IServiceProvider serviceProvider) {
            this.logger.LogInformation("EnsureCreatedAndSeeded");
            
            CreateDefaultRolesAsync(serviceProvider);
            CreateDefaultUsersAsync(serviceProvider);
        }
        
        /// <summary>
        /// Creates default roles for the application
        /// </summary>
        private async void CreateDefaultRolesAsync(IServiceProvider serviceProvider) {
            this.logger.LogInformation("CreateDefaultRolesAsync");
            
            var options = serviceProvider.GetService<IdentityOptions>();

            var claimType = options.ClaimsIdentity.RoleClaimType;            
            
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
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
        /// Creates the default users for the application
        /// </summary>
        private void CreateDefaultUsersAsync(IServiceProvider serviceProvider) {
            this.logger.LogInformation("CreateDefaultUsers");            
        } // end CreateAdminUser
	} // end class
} // end namespace