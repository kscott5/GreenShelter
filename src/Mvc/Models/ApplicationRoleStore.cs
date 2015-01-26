using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationRoleStore : RoleStore<ApplicationRole, GreenShelterDbContext> {
		public ApplicationRoleStore(GreenShelterDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
		{
		}			
    } // end class
} // end namespace