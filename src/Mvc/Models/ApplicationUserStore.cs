using Microsoft.AspNet.Identity;
using AspNet.Identity.MongoDB;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationUserStore : UserStore<ApplicationUser> {		
		/// <summary>
		///
		/// </summary>
		public ApplicationUserStore(IdentityContext context) : base(context) {
		}
    } // end class
} // end namespace