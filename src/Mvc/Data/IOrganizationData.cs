using System;
using System.Collections.Generic;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Data {
	/// <summary>
	/// Interface used to perform data operation for a specific organization 
	/// </summary>
	public interface IOrganizationData {		
		/// <summary>
		/// Adds a new organization's information to the application
		/// </summary>
		void AddNewInfo();
		
		/// <summary>
		/// Updates an exist an organization's application information
		/// </summary>
		void UpdateInfo();

		/// <summary>
		/// Retrieves an organization's information from the application
		/// </summary>
		void GetMe();
				
		/// <summary>
		/// Actives/De-actives an existing organization's application information
		/// </summary>
		void Activate();
		
		/// <summary>
		/// Determines if an existing client's application information is active
		/// </summary>
		bool IsActive();
		
		/// <summary>
		/// Retrieves a list of organizations from the application
		/// </summary>
		void GetList();
		
		/// <summary>
		/// Generates an organization secret key used access specific application information
		/// </summary>
		void GenerateSecretKey();
	}
}