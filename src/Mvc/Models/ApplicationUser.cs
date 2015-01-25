using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;

namespace PCSC.GreenShelter.Models {
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser   {
		public virtual List<Address> Addresses {get; set;}
		public virtual List<Organization> Organizations {get; set;}
		
		public virtual bool Active {get; set;}
		public virtual DateTime LastActive {get; set;}
		
		public virtual DateTime Creation {get; set;}
		
		public virtual DateTime Modified {get; set;}
		public virtual string ModifiedById {get; set;}
		
		public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    } // end class
} // end namespace