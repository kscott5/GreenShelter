using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNet.Identity;

﻿namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationRole : IdentityRole<int> {
		[Key]
		//[Column("RoleId")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override int Id {get; set;}
		
		public virtual string Description {get;set;}
	} // end class 
} // end namespace