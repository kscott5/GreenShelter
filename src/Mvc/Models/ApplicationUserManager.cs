using Microsoft.AspNet.Identity;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationUserManager : UserManager<ApplicationUser> {		
		/// <summary>
		///
		/// </summary>
		public ApplicationUserManager(ApplicationUserStore store) : base(store) {
		}
    } // end class
} // end namespace