using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, GreenShelterDbContext, int> {
		public ApplicationUserStore(GreenShelterDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}
    } // end class
} // end namespace