using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;

using Microsoft.AspNet.Security;
using Microsoft.AspNet.Security.Cookies;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Models {
	public class ApplicationSignInManager : SignInManager<ApplicationUser> , IGreenShelterApplication {
		public string TagName {get { return "ApplicationSignInManager";}}
		
		public ApplicationSignInManager(ApplicationUserManager userManager, IHttpContextAccessor contextAccessor, IClaimsIdentityFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor = null, ILoggerFactory loggerFactory = null)  : 
			base(userManager, contextAccessor, claimsFactory, optionsAccessor, loggerFactory) {
				UserManager = userManager;
		}
		
		public new ApplicationUserManager UserManager { get; private set;}
		
		public virtual async Task<IdentityResult> RegisterClientAsync(ApplicationUser user, string password, CancellationToken cancellationToken = default(CancellationToken)) {
			var result = await this.UserManager.CreateClientAsync(user, password);
			
			if(result != IdentityResult.Success)
				return result;
			
			return await this.SignInAsync(user.UserName, password, false);
		}
		
		public override async Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null, CancellationToken cancellationToken = default(CancellationToken)) {
			await Task.Run(() => {
				throw new NotImplementedException("Not sure if used by external providers");
			});
		}
		
		public virtual async Task<IdentityResult> SignInAsync(string username, string password, bool isPersistent, CancellationToken cancellationToken = default(CancellationToken)) {
			var user = await this.UserManager.FindByNameAsync(username);
			
			if(user == null) 
				return IdentityResult.Failed(IdentityErrorDescriber.Default.InvalidUserName(username));
			
			if(!await UserManager.CheckPasswordAsync(user, password))
				return IdentityResult.Failed(IdentityErrorDescriber.Default.PasswordMismatch());
		
			ClaimsIdentity claimsIdentity = await this.CreateUserIdentityAsync(user, default(CancellationToken));
			claimsIdentity.AddClaim(new Claim(ApplicationClaimsType.Password, "Password"));
			
			HttpResponse arg_C2_0 = this.Context.Response;
			AuthenticationProperties expr_B5 = new AuthenticationProperties();
			expr_B5.IsPersistent = isPersistent;
			arg_C2_0.SignIn(expr_B5, claimsIdentity);
			
			return IdentityResult.Success;
		}
		
		public override void SignOut() {
			base.SignOut();
			
			//this.Context.Response.SignOut(CookieAuthenticationDefaults.AuthenticationType);
		}
	} // end class
} // end namespace