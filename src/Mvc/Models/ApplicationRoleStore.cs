using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationRoleStore : RoleStore<ApplicationRole> {		
		/// <summary>
		///
		/// </summary>
		public ApplicationRoleStore(IdentityContext context) : base(context) {
		}
    } // end class
} // end namespace