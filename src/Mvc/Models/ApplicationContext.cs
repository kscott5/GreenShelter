using System;
using AspNet.Identity.MongoDB;
using MongoDB.Driver;

using PCSC.GreenShelter.Extensions;

﻿namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// 
	/// </summary>
	public class ApplicationContext : IdentityContext, IGreenShelterApplication, IDisposable {
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "ApplicationIdentityContext"; } }
		
		public ApplicationContext() : base() {
		}
		
		public ApplicationContext(MongoCollection users) : base(users){
			
		}
		
		public ApplicationContext(MongoCollection users, MongoCollection roles) : base(users, roles) {
		}

		public void Dispose() {
		}
	} // end class 
} // end namespace