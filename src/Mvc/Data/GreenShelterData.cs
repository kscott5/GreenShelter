using System;
using System.Collections.Generic;

using AspNet.Identity.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Data {
	/// <summary>
	/// Provides the the data access for the Green Shelter Application
	/// </summary>
	public class GreenShelterData: IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "GreenShelterData" ; } }
		
		private static ApplicationContext _context;
		public static ApplicationContext GetContext () {
			return _context;
		}
		
		private static ApplicationUserManager _userManager;
		public static ApplicationUserManager GetUserManager() {
			return _userManager;
		}
		
		private static ApplicationRoleManager _roleManager;
		public static ApplicationRoleManager GetRoleManager() {
			return _roleManager;
		}
		
		public GreenShelterData() {}
		static GreenShelterData() {	
			var data = new GreenShelterData();
			
			// Reference: https://github.com/g0t4/aspnet-identity-mongo
			var client = new MongoClient( data.ConnectionString() );
			var database = client.GetServer().GetDatabase( data.DatabaseName() );
			
			var users = database.GetCollection<ApplicationUser>("users");
			var roles = database.GetCollection<ApplicationUser>("roles");
			
			_context = new ApplicationContext(users, roles);
			
			var userstore = new ApplicationUserStore(_context);
			_userManager = new ApplicationUserManager(userstore);
			
			var rolestore = new ApplicationRoleStore(_context);
			_roleManager = new ApplicationRoleManager(rolestore);
			
			IndexChecks.EnsureUniqueIndexOnUserName(users);
			IndexChecks.EnsureUniqueIndexOnEmail(users);
		}
	} // end class
} // end namespace