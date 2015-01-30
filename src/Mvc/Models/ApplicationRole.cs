using System;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;

﻿namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationRole : IdentityRole {
		public virtual string Description {get;set;}
	} // end class 
} // end namespace