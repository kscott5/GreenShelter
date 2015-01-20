using System;
using System.Collections.Generic;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Data {	
	/// <summary>
	/// Interface used to perform database operation for an organization's client list
	/// </summary>
	public interface IClientData {		
		/// <summary>
		/// Adds a new client to the application
		/// </summary>
		void AddNewInfo();
		
		/// <summary>
		/// Updates an exist client's application information
		/// </summary>
		void UpdateInfo();
		
		/// <summary>
		/// Retrieves a client's information from the application
		/// </summary>
		void GetMe();		

		/// <summary>
		/// Actives/De-actives an existing client's application information
		/// </summary>
		void Activate();
		
		/// <summary>
		/// Determines if an existing client's application information is active
		/// </summary>
		bool IsActive();
		
		/// <summary>
		/// Retrieves a list of clients from the application
		/// </summary>
		void GetList();
		
		/// <summary>
		/// Retrieves an organization's list of clients from the application
		/// </summary>
		void GetList(int orgId);
	}
}