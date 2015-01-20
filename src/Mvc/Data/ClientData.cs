using System;
using System.Collections.Generic;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Data {
	/// <summary>
	/// Concrete implementation of the <cref="IClientData"/> interface used to 
	///	perform database operation for an organization's client list
	/// </summary>
	public class ClientData : IClientData {
		/// <summary>
		/// Adds a new client to the application
		/// </summary>
		public void AddNewInfo(){
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Updates an exist client's application information
		/// </summary>
		public void UpdateInfo(){
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Retrieves a client's information from the application
		/// </summary>
		public void GetMe(){
			throw new NotImplementedException();
		}

		/// <summary>
		/// Actives/De-actives an existing client's application information
		/// </summary>
		public void Activate(){
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Determines if an existing client's application information is active
		/// </summary>
		public bool IsActive(){
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Retrieves a list of clients from the application
		/// </summary>
		public void GetList(){
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Retrieves an organization's list of clients from the application
		/// </summary>
		/// <param name="orgId"></param>
		public void GetList(int orgId) {
			throw new NotImplementedException();
		}
	}
}