using System;
using System.Collections.Generic;
using MongoDB.AspNet.Identity;

namespace PCSC.GreenShelter.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class SystemUser : IdentityUser   {
		public virtual List<Address> Addresses {get; set;}
		public virtual List<Organization> Organizations {get; set;}
		
		public virtual bool Active {get; set;}
		public virtual DateTime LastActive {get; set;}
		
		public virtual DateTime Creation {get; set;}
		
		public virtual DateTime Modified {get; set;}
		public virtual string ModifiedById {get; set;}
    }
}