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
		}
		
		public override void SignOut() {
			base.SignOut();
			
			this.Context.Response.SignOut(CookieAuthenticationDefaults.AuthenticationType);
		}
	} // end class
} // end namespace