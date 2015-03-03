using System;
using System.Security.Claims;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	///
	/// </summary>
	public class ApplicationUserManager : UserManager<ApplicationUser>, IGreenShelterApplication {		
		public string TagName {get {return "ApplicationUserManager"; }}
		
		/// <summary>
		///
		/// </summary>
		public ApplicationUserManager(ApplicationUserStore store) : base(store) {
		}
		
		/// <summary>
		///     Create a user with specific role access
		/// </summary>
		/// <param name="user"></param>
		/// <param name="roleName"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
			if(user != null && !string.IsNullOrEmpty(user.SSNo)) {
				// TODO: Create a separate Hasher for Social Security Number
				try {
					user.SSNo = PasswordHasher.HashPassword(user, user.SSNo); 
				} catch(Exception ex) {
					throw new Exception("Error trying to encrypt the SS #", ex);
				}
			}

			user.GuidId = Guid.NewGuid().ToString("N");
			
			var result = await this.CreateAsync(user);
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddToRoleAsync(user, roleName); 
			if(result != IdentityResult.Success) 
				return result;
			
			return await CreateUserClaimsAsync(user, roleName, cancellationToken);
		}		
		
		/// <summary>
		///     Create a user and associates it with the given password and role access
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="roleName"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		private async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
			if(user != null && !string.IsNullOrEmpty(user.SSNo)) {
				// TODO: Create a separate Hasher for Social Security Number
				try {
						user.SSNo = PasswordHasher.HashPassword(user, user.SSNo); 
				} catch(Exception ex) {
					throw new Exception("Error trying to encrypt the SS #", ex);
				}
			}

			user.GuidId = Guid.NewGuid().ToString("N");
			
			var result = await this.CreateAsync(user, password);
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddToRoleAsync(user, roleName); 
			if(result != IdentityResult.Success) 
				return result;
			
			return await CreateUserClaimsAsync(user, roleName, cancellationToken);
		}
		
		private async Task<IdentityResult> CreateUserClaimsAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken = default(CancellationToken)) {
			var claim = this.Options.ClaimsIdentity;
			var result = await this.AddClaimAsync(user, new Claim(claim.RoleClaimType, roleName));
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddClaimAsync(user, new Claim(claim.UserIdClaimType, user.Id.ToString()));
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddClaimAsync(user, new Claim(ApplicationClaimsType.GuidId, user.GuidId));
			if(result != IdentityResult.Success)
				return result;
			
			return result;
		}
		
		/// <summary>
		///     Create an admin user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateAdminAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, ApplicationRole.Administrator, cancellationToken);
		}
		
		/// <summary>
		///     Create an admin user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateAdminAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, password, ApplicationRole.Administrator, cancellationToken);
		}
		
		/// <summary>
		///     Create an organization user
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateOrganizationAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, ApplicationRole.Organization, cancellationToken);
		}
		
		/// <summary>
		///     Create an organization user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateOrganizationAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, password, ApplicationRole.Organization, cancellationToken);
		}
		
		/// <summary>
		///     Create a volunteer user
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateVolunteerAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, ApplicationRole.Volunteer, cancellationToken);
		}
		
		/// <summary>
		///     Create a volunteer user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateVolunteerAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, password, ApplicationRole.Volunteer, cancellationToken);
		}
		
		/// <summary>
		///     Create a client user
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateClientAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, ApplicationRole.Client, cancellationToken);
		}
		
		/// <summary>
		///     Create a client user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateClientAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			return await this.CreateUserAsync(user, password, ApplicationRole.Client, cancellationToken);
		}
		
		public virtual async Task<ApplicationUser> FindByGuidIdAsync(string guidId, CancellationToken cancellationToken = default(CancellationToken)) {
			return await ((ApplicationUserStore)this.Store).FindByGuidIdAsync(guidId, cancellationToken);
		} 
    } // end class
} // end namespace