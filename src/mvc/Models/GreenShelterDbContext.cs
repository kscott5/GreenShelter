using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
			base.OnConfiguring(builder);
		}
	} // end class
} // end namespace