using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNet.Identity.EntityFramework;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationRole : IdentityRole<int> {
		public static readonly string Administrator = "Administrator";
		public static readonly string Organization = "Organization";
		public static readonly string Volunteer = "Volunteer";
		public static readonly string Client = "Client";
		
		[Key]
		[Column("RoleId")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override int Id {get; set;}
		
		public virtual string Description {get;set;}
	} // end class 
} // end namespace