using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNet.Identity;

namespace PCSC.GreenShelter.Models {
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	[Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser<int>   {
		
		[Key]
		//[Column("UserId")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override int Id {get; set;}
		
		//[ForeignKey("RoleId")]
		public virtual ApplicationRole Role {get; set;}
		
		public virtual string FirstName {get; set; }
		public virtual string LastName {get; set; }
		
		public virtual List<Address> Addresses {get; set;}
		public virtual List<Organization> Organizations {get; set;}
		
		public virtual string SSNo { get; set; }
		public virtual bool Active {get; set;}
		public virtual DateTime LastActive {get; set;}
		
		public virtual DateTime CreationDate {get; set;}
		
		public virtual DateTime? ModifiedDate {get; set;}
		
		//[ForeignKey(UserId)]
		public virtual int? ModifiedByUserId {get; set;}		
    } // end class
} // end namespace