using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.Framework.OptionsModel;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Models {
	/// <summary>
	/// Provides methods to create a claims identity for a given user.
	/// </summary>
	/// <typeparam name="TUser">The type used to represent a user.</typeparam>
	/// <typeparam name="TRole">The type used to represent a role.</typeparam>
	public class ApplicationClaimsIdentityFactory : ClaimsIdentityFactory<ApplicationUser, ApplicationRole>, IGreenShelterApplication {
		public string TagName { get {return "ApplicationClaimsIdentityFactory"; }}
				
		/// <summary>
		/// Initializes a new instance of the <see cref="!:ClaimsIdentityFactory" /> class.
		/// </summary>
		/// <param name="userManager">The <see cref="T:PCSC.GreenShelter.Models.ApplicationUserManager`1" /> to retrieve user information from.</param>
		/// <param name="roleManager">The <see cref="T:PCSC.GreenShelter.Models.ApplicationRoleManager`1" /> to retrieve a user's roles from.</param>
		/// <param name="optionsAccessor">The configured <see cref="T:Microsoft.AspNet.Identity.IdentityOptions" />.</param>
		public ApplicationClaimsIdentityFactory(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor) {
		}
		
		// <summary>
		/// Create a client user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateAdminAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			this.WriteInformation("Creating Adminstrator User Async");
			
			user.Role = await this.RoleManager.FindByNameAsync("Administrator");
			if(user.Role == null)
				throw new Exception("Administrator role not defined");
			
			var result = await this.UserManager.CreateAsync(user, password);
			if(result == IdentityResult.Success) {
				await this.UserManager.AddToRoleAsync(user, user.Role.Name); 				
				await this.CreateAsync(user);
			}
			
			return result;
		}

		/// <summary>
		/// Create a organization user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateOrganizationAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			this.WriteInformation("Creating Organization User Async");
			
			user.Role = await this.RoleManager.FindByNameAsync("Organization");
			if(user.Role == null)
				throw new Exception("Organization role not defined");
						
			var result = await this.UserManager.CreateAsync(user, password);
			if(result == IdentityResult.Success) {
				await this.UserManager.AddToRoleAsync(user, user.Role.Name); 				
				await this.CreateAsync(user);
			}
			
			return result;
		}		
		
		/// <summary>
		/// Create a client user and associates it with the given password
		/// </summary>
		/// <param name="user"></param>
		/// <param name="password"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async Task<IdentityResult> CreateClientAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			this.WriteInformation("Creating Client User Async");
			
			user.Role = await this.RoleManager.FindByNameAsync("Client");
			if(user.Role == null)
				throw new Exception("Client role not defined");
			
			var result = await this.UserManager.CreateAsync(user, password);
			if(result == IdentityResult.Success) {
				await this.UserManager.AddToRoleAsync(user, user.Role.Name); 				
				await this.CreateAsync(user);
			}
			
			return result;
		}		
	} // end class
} // end namespace