using System;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// Provides the the data access for the Green Shelter Application
	/// </summary>
	public class GreenShelterDbContext: IdentityDbContext<ApplicationUser, ApplicationRole, string>, IGreenShelterApplication {
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "GreenShelterDbContext"; } }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Organization> Organizations { get; set; }

		protected override void OnModelCreating(ModelBuilder builder) {
			builder.Entity<Address>().Key(a => a.AddressId);
			builder.Entity<Organization>().Key(a => a.OrganizationId);

			//builder.Entity<ApplicationUser>().ForRelational(u => u.Addresses);
			//builder.Entity<ApplicationUser>().OneToMany(u => u.Organizations);

			base.OnModelCreating(builder);
		}

		protected override void OnConfiguring(DbContextOptions options) {
			options.UseSqlServer();
		}
	} // end class
} // end namespace