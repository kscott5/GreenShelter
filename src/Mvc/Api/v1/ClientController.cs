using System;
using System.Security;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Api.v1 {
    //[Authorize]
	[Route("api/v1/client", Name = "Client")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ClientController : Controller, IGreenShelterApplication {
		
		public ClientController(ApplicationSignInManager signInManager, ApplicationUserManager userManager) {
           SignInManager = signInManager;
		   UserManager = userManager;
        }

        public ApplicationSignInManager SignInManager { get; private set; }
		public ApplicationUserManager UserManager {get; private set;}
		
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "ClientController" ; } }

		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
        [Route("ExternalLogin", Name="ExternalLogin")]
        public IActionResult ExternalLogin(string authenticationType, string returnUrl = null) {		
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action("externallogincallback", "client", new { ReturnUri = returnUrl });

            var properties = SignInManager.ConfigureExternalAuthenticationProperties(authenticationType, redirectUrl);
            return new ChallengeResult(authenticationType, properties);
        }

        [HttpGet]
        [AllowAnonymous]
		[Route("ExternalLoginCallback", Name = "ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null) {
            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction("StartPage", "SPA");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                // If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = info.LoginProvider;
                // REVIEW: handle case where email not in claims?
                var email = info.ExternalIdentity.FindFirst(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", email );
            }
        }

		[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
		[Route("ExternalLoginConfirmation", Name="ExternalLoginConfirmation")]
        public async Task<IActionResult> ExternalLoginConfirmation(string email, string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("profile", "api/v1/client");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await SignInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = email, Email = email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                //AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
		 private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("StartPage", "SPA");
            }
        }
		
        [HttpGet]
		//[AllowAnonymous]
        [Route("AuthTypes", Name="AuthTypes")]
        public JsonResult AuthenticationTypes() {
			var response = new ApiResponse();
			
			try {
				response.Code = 200;
				response.Description = "Retreiving authtypes was successful";
				response.Data = SignInManager.GetExternalAuthenticationTypes().ToList();				
			} catch(Exception ex) {
				response.Code = 400;
				response.Description = "Retreiving authtypes was unsuccessful";
				
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
        }
		
		[HttpPost]
		//[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		[Route("Login", Name="Login")]
		public async Task<JsonResult> Login([FromBody] Login model, string returnUrl = null) {
			var response = new ApiResponse{Code = 400};
			this.Context.Response.StatusCode = response.Code;
			
			try {
				var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
				if(result != SignInResult.Success) {
					response.Data = result;
					response.Description = "Invalid username and/or password";
					return new JsonResult(response);
				}
				
				var user = await UserManager.FindByNameAsync(model.UserName);
				response.Data =  new { IsAuthenticated = true, GuidId = user.GuidId, returnUrl = returnUrl };				
				response.Description = "Login was successful";
				
				this.Context.Response.StatusCode = response.Code = 200;
				
				// SignInManager.SignOut();
				
				// var result = await SignInManager.SignInAsync(model.UserName, model.Password, model.RememberMe);
				// if(result != IdentityResult.Success) {
					// response.Data = result.Errors;
					// response.Description = "Login was unsuccessful";
					// return new JsonResult(response);
				// }
				
				// AppUtilityHelper.Logger.WriteInformation("AuthenticationType: {0}, IsAuthenticated: {1}", 
					// ClaimsPrincipal.Current.Identity.AuthenticationType,
					// ClaimsPrincipal.Current.Identity.IsAuthenticated);
					 
				// var isAuthenticated = this.Context.User.Identity.IsAuthenticated;
				// if(!isAuthenticated) {
					// response.Description = string.Format("Could not authenticate the username: {0}", model.UserName);
					// return new JsonResult(response);
				// }
				
				// var guidIdClaim = this.Context.User.FindFirst(ApplicationClaimsType.GuidId);
				// var userNameClaim = this.Context.User.FindFirst(ApplicationClaimsType.UserName);
				
				// response.Data =  new { IsAuthenticated = isAuthenticated, GuidId = guidIdClaim.Value };				
				// response.Description = "Login was successful";
				
				// this.Context.Response.StatusCode = response.Code = 200;
			} catch(Exception ex) {
				response.Description = "Exception: Login was unsuccessful";
				
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
		}
		
		[HttpPost]
		//[AllowAnonymous]
		[Route("Register", Name="Register")]
		public async Task<JsonResult> Register([FromBody] Registration model) {
			var response = new ApiResponse ();
			
			try {
				SignInManager.SignOut();
				
				var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName, SSNo = model.SSNo };
                var result = await SignInManager.RegisterClientAsync(user, model.Password);
				
				response.Code = 200;
                bool isAuthenticated = this.Context.User.Identity.IsAuthenticated;
				if(result == IdentityResult.Success && isAuthenticated) {
					var guidIdClaim = this.Context.User.FindFirst(ApplicationClaimsType.GuidId);
					
					response.Data =  new { IsAuthenticated = isAuthenticated, GuidId = guidIdClaim.Value };
					response.Description = string.Format("{0} registered successfully", model.UserName);
				} else {
					response.Description = string.Format("{0} registration failed", model.UserName);
					response.Data= result.Errors;
				}
			} catch(Exception ex) {
				response.Code = 400;
				response.Description = string.Format("Failed to register new user {0}", model.UserName);
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
			return new JsonResult(response);
		}
		
		[Route("Me/{guidid?}", Name="Me")]
		public async Task<JsonResult> Me(string guidId) {
			var response = new ApiResponse { Code = 400};
			this.Context.Response.StatusCode = response.Code;
			
			try {
				if(!string.IsNullOrEmpty(guidId)) {
					var user = await UserManager.FindByGuidIdAsync(guidId);
					
					if(user != null) {
						
						response.Data = new { FirstName = user.FirstName, LastName = user.LastName, PhoneNumber = user.PhoneNumber}  ;
						response.Description = "Retrieved my profile successful";			
						this.Context.Response.StatusCode = response.Code = 200;
					} else {
						response.Description = "Failed to retrieve your profile information";
					}
				}
			} catch(Exception ex) {
				response.Description = "Failed to retrieve me";					
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
		
			return  new JsonResult(response);
		}
	} // end class
} // end namespace