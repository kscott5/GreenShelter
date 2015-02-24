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
			var result = await this.CreateAsync(user);
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddToRoleAsync(user, roleName); 
			if(result != IdentityResult.Success) 
				return result;
			
			var claim = this.Options.ClaimsIdentity;
			result = await this.AddClaimAsync(user, new Claim(claim.RoleClaimType, roleName));
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddClaimAsync(user, new Claim(claim.UserIdClaimType, user.Id.ToString()));
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddClaimAsync(user, new Claim(claim.UserNameClaimType, user.UserName));
			if(result != IdentityResult.Success)
				return result;

			return result;
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
			var result = await this.CreateAsync(user, password);
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddToRoleAsync(user, roleName); 
			if(result != IdentityResult.Success) 
				return result;
			
			var claim = this.Options.ClaimsIdentity;
			result = await this.AddClaimAsync(user, new Claim(claim.RoleClaimType, roleName));
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddClaimAsync(user, new Claim(claim.UserIdClaimType, user.Id.ToString()));
			if(result != IdentityResult.Success)
				return result;
			
			result = await this.AddClaimAsync(user, new Claim(claim.UserNameClaimType, user.UserName));
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
			this.WriteInformation("Creating Administrator User Async");
			
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
			this.WriteInformation("Creating Administrator User Async");
			
			return await this.CreateUserAsync(user, password, ApplicationRole.Administrator, cancellationToken);
		}
		
		/// <summary>
		///     Create an organization user
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateOrganizationAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken)) {
			this.WriteInformation("Creating Organization User Async");
			
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
			this.WriteInformation("Creating Organization User Async");
			
			return await this.CreateUserAsync(user, password, ApplicationRole.Organization, cancellationToken);
		}
		
		/// <summary>
		///     Create a client user
		/// </summary>
		/// <param name="user"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateClientAsync(ApplicationUser user, CancellationToken cancellationToken = default(CancellationToken)) {
			this.WriteInformation("Creating Client User Async");
			
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
			this.WriteInformation("Creating Client User Async");
			
			return await this.CreateUserAsync(user, password, ApplicationRole.Client, cancellationToken);
		}
    } // end class
} // end namespace