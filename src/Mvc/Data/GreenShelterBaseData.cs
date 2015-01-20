using System;
using System.Collections.Generic;

using MongoDB.AspNet.Identity;
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
	public abstract class GreenShelterBaseData: IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "GreenShelterBaseData" ; } }

		protected MongoClient Client { get; set; }
		protected MongoDatabase Database { get; set; }
		
		protected MongoCollection<SystemUser> Users { get; set; }
				
		protected GreenShelterBaseData() {
			lock(this) {
				if(Client == null) {
					Client = new MongoClient( this.ConnectionString() );
					Database = Client.GetServer().GetDatabase( this.DatabaseName() );
					Users = Database.GetCollection<SystemUser>("users");
				}
			}
		}
	} // end class
} // end namespace