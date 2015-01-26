using System;
using System.Collections.Generic;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Data {
	/// <summary>
	/// Concrete implementation of the IOrganizationData interface used to 
	///	perform database operation for an organization's client list
	/// </summary>
	public class OrganizationData : IOrganizationData {
		/// <summary>
		/// Adds a new organization's information to the application
		/// </summary>
		public void AddNewInfo() {
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Updates an exist an organization's application information
		/// </summary>
		public void UpdateInfo() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Retrieves an organization's information from the application
		/// </summary>
		public void GetMe() {
			throw new NotImplementedException();
		}
				
		/// <summary>
		/// Actives/De-actives an existing organization's application information
		/// </summary>
		public void Activate() {
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Determines if an existing client's application information is active
		/// </summary>
		public bool IsActive() {
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Retrieves a list of organizations from the application
		/// </summary>
		public void GetList() {
			throw new NotImplementedException();
		}
		
		/// <summary>
		/// Generates an organization secret key used access specific application information
		/// </summary>
		public void GenerateSecretKey() {
			throw new NotImplementedException();
		}
	}
}