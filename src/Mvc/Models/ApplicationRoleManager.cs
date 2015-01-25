using Microsoft.AspNet.Identity;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationRoleManager : RoleManager<ApplicationRole> {		
		/// <summary>
		///
		/// </summary>
		public ApplicationRoleManager(ApplicationRoleStore store) : base(store) {
		}
    } // end class
} // end namespace