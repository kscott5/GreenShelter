using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

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
		
		public GreenShelterDbContext(DbContextOptions<GreenShelterDbContext> options, ILogger<GreenShelterDbContext> logger) : base(options) {
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
	} // end class
} // end namespace