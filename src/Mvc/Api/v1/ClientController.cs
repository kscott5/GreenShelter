using System;
using System.Security;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Api.v1 {
    [Authorize]
	[Route("api/v1/client", Name = "Client")]
	public class ClientController : Controller, IGreenShelterApplication {
	
		public ClientController(SignInManager<ApplicationUser> signInManager, ApplicationUserManager userManager) {
           SignInManager = signInManager;
		   UserManager = userManager;
        }

        public SignInManager<ApplicationUser> SignInManager { get; private set; }
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
			this.WriteInformation("AuthenticationType: {0}, returnUrl: {1}", authenticationType, returnUrl);
			
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action("externallogincallback", "client", new { ReturnUri = returnUrl });

            var properties = SignInManager.ConfigureExternalAuthenticationProperties(authenticationType, redirectUrl);
            return new ChallengeResult(authenticationType, properties);
        }

        [HttpGet]
        [AllowAnonymous]
		[Route("ExternalLoginCallback", Name = "ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null) {
			this.WriteInformation("ExternalLoginCallback");
			
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
		[AllowAnonymous]
        [Route("AuthTypes", Name="AuthTypes")]
        public JsonResult AuthenticationTypes() {
			this.WriteInformation("AuthenticationTypes");
			
			var response = new ApiResponse();
			
			try {
				response.Code = 200;
				response.Description = "Retreiving authtypes was successful";
				response.Data = SignInManager.GetExternalAuthenticationTypes().ToList();				
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Retreiving authtypes was unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
        }
		
		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		[Route("Login", Name="Login")]
		public async Task<JsonResult> Login([FromBody] Login model) {
			this.WriteInformation("Login UserName: {0}: Remember Me: {1}", model.UserName, model.RememberMe);
						
			var response = new ApiResponse();
			
			try {
				SignInManager.SignOut();
				var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
				
				response.Code = 200;
				if(result == SignInResult.Success) {
					var user = await SignInManager.UserManager.FindByNameAsync(model.UserName);
					//var identity = await SignInManager.CreateUserIdentityAsync(user);
					
					response.Data = new { /*identity = identity,*/ id = user.Id, UserName  = user.UserName, firstname = user.FirstName, lastname = user.LastName};
					response.Description = "Login was successful";
				} else {
					response.Data = result;
					response.Description = "Login was unsuccessful";
				}
			} catch(Exception ex) {
				response.Code = 401;
				this.Context.Response.StatusCode = 401;
				response.Description = "Login was unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
		}
		
		[HttpPost]
		[AllowAnonymous]
		[Route("Register", Name="Register")]
		public async Task<JsonResult> Register([FromBody] Registration model) {
			this.WriteInformation("Registering {0}", model.UserName);
			
			var response = new ApiResponse ();
			
			try {
				var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
				
                var result = await UserManager.CreateClientAsync(user, model.Password);
				
				response.Code = 200;
                if (result.Succeeded) {
					response.Description = string.Format("{0} registered successfully", model.UserName);
					response.Data = model.UserName; // What should we pass back
				} else {
					response.Description = string.Format("{0} registration failed", model.UserName);
					response.Data= result.Errors;
				}
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = string.Format("Failed to register new user {0}", model.UserName);
				this.WriteError(response.Description, ex);
			}
			return new JsonResult(response);
		}
		
		[HttpGet]
		[Route("Me", Name="Me")]
		public async Task<JsonResult> Me(string id) {
			this.WriteInformation("Me id: {0}", id);
			
			var response = new ApiResponse();
			
			try {
				response.Code = 200;
				var user = await SignInManager.UserManager.FindByIdAsync(id);
				if(user != null) {
					response.Data = new { address = "", organization = ""};
					response.Description = "Retreiving me was successful";			
				} else {
					response.Description = "Failed to retrieve me";			
				}
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Failed to retrieve me";	
				
				this.WriteError(response.Description, ex);
			}
		
			return  new JsonResult(response);
		}
		
		[HttpPost]
		[Route("Me", Name="Me")]
		public JsonResult Me([FromBody] ApplicationUser user) {
			this.WriteInformation("Me [POST] email: {0}", user.Email);
			
			var response = new ApiResponse {
				Data = user.Email
			};
			
			try {
				response.Code = 200;
				response.Description = "Updating me was successful";			
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Updating me was unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
					
			return  new JsonResult(response);
		}
	} // end class
} // end namespace