using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, GreenShelterDbContext, int> {
		public ApplicationUserStore(GreenShelterDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)	{
		}
		
		public virtual async Task<ApplicationUser> FindByGuidIdAsync(string guidId, CancellationToken cancellationToken = default(CancellationToken)) {
			cancellationToken.ThrowIfCancellationRequested();
			
			return await this.Users.FirstOrDefaultAsync(u => u.GuidId == guidId, cancellationToken);
		} 
    } // end class
} // end namespace