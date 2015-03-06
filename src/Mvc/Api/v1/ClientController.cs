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
        public IActionResult ExternalLogin(string AuthenticationType, string returnUrl = null) {		
            // Request a redirect to the external login provider
            var redirectUrl = Url.Action("externallogincallback", "client", new { ReturnUri = returnUrl });

            var properties = SignInManager.ConfigureExternalAuthenticationProperties(AuthenticationType, redirectUrl);
            return new ChallengeResult(AuthenticationType, properties);
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

            // Sign in the User with this external login provider if the User already has a login
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
                // If the User does not have an account, then prompt the User to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.LoginProvider = info.LoginProvider;
                // REVIEW: handle case where Email not in claims?
                var Email = info.ExternalIdentity.FindFirst(ClaimTypes.Email);
                return View("ExternalLoginConfirmation", Email );
            }
        }

		[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
		[Route("ExternalLoginConfirmation", Name="ExternalLoginConfirmation")]
        public async Task<IActionResult> ExternalLoginConfirmation(string Email, string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Profile", "api/v1/client");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the User from the external login provider
                var info = await SignInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var User = new ApplicationUser { UserName = Email, Email = Email };
                var result = await UserManager.CreateAsync(User);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(User, info);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(User, isPersistent: false);
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
		public async Task<JsonResult> Login([FromBody] ApplicationUser model, string returnUrl = null) {
			var response = new ApiResponse{Code = 400};
			this.Context.Response.StatusCode = response.Code;
			
			try {
				var result = await SignInManager.PasswordSignInAsync(model.UserName, model.PasswordHash, isPersistent: false, shouldLockout: false);
				if(result != SignInResult.Success) {
					response.Data = result;
					response.Description = "Invalid UserName and/or Password";
					return new JsonResult(response);
				}
				
				var user = await UserManager.FindByNameAsync(model.UserName);
				response.Data =  user.CreateData(true, returnUrl);
				response.Description = "Login was successful";
				
				this.Context.Response.StatusCode = response.Code = 200;
			} catch(Exception ex) {
				response.Description = "Exception: Login was unsuccessful";
				
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
		}
		
		[HttpPost]
		//[AllowAnonymous]
		[Route("Register", Name="Register")]
		public async Task<JsonResult> Register([FromBody] ApplicationUser model) {
			var response = new ApiResponse { Code = 400	};
			this.Context.Response.StatusCode = response.Code;
			
			try {
				SignInManager.SignOut();
				
				var result = await SignInManager.RegisterClientAsync(model, model.PasswordHash);
				if(result == IdentityResult.Success) {
					var guidIdClaim = this.Context.User.FindFirst(ApplicationClaimsType.GuidId);
					model.GuidId = guidIdClaim.Value;
					
					response.Code = 200;
					response.Data =  model.CreateData(true);
					response.Description = string.Format("{0} registered successfully", model.UserName);
					this.Context.Response.StatusCode = response.Code;
				} else {
					response.Description = string.Format("{0} Registration failed", model.UserName);
					response.Data= result.Errors;
				}
			} catch(Exception ex) {
				response.Description = string.Format("Failed to register new User {0}", model.UserName);
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
			return new JsonResult(response);
		}
		
		[Route("Me/{guidId?}", Name="Me")]
		public async Task<JsonResult> Me(string guidId, [FromBody] ApplicationUser model = null) {
			var method = this.Context.Request.Method.Trim().ToUpper();
			switch(method) {
				case "GET": return await GetMe(guidId);
				case "POST": return await PostMe(guidId, model);
				default:
					var response = new ApiResponse {
						Code = 401,
						Description = string.Format("Http verb {0} not supported. Try using GET or POST", method)
					};
					
					this.Context.Response.StatusCode = response.Code;
					return new JsonResult(response);
			}
		} // end Me
		
		private async Task<JsonResult> GetMe(string guidId) {
			var response = new ApiResponse { Code = 400};
			this.Context.Response.StatusCode = response.Code;
			
			try {
				if(!string.IsNullOrEmpty(guidId)) {
					var user = await UserManager.FindByGuidIdAsync(guidId);
					
					if(user != null) {
						response.Data = user.CreateData(true);
						response.Description = "Retrieved my Profile successful";			
						this.Context.Response.StatusCode = response.Code = 200;
					} else {
						response.Description = "Failed to retrieve your Profile information";
					}
				}
			} catch(Exception ex) {
				response.Description = "Failed to retrieve me";					
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
		
			return  new JsonResult(response);
		} // end GetMe
		
		private async Task<JsonResult> PostMe(string guidId, ApplicationUser model) {
			var response = new ApiResponse { Code = 400};
			this.Context.Response.StatusCode = response.Code;
			
			try {
				if(!string.IsNullOrEmpty(guidId) && guidId.Equals(model.GuidId)) {
					var user = await UserManager.FindByGuidIdAsync(guidId);
					
					user.FirstName = model.FirstName;
					user.LastName = model.LastName;;
					user.PhoneNumber = model.PhoneNumber;
					
					// TODO: Use a Binding Filter for PhoneNumberType
					PhoneNumberType phoneNumberType;
					if(!string.IsNullOrEmpty(model.PhoneNumber) && Enum.TryParse<PhoneNumberType>(model.PhoneNumberType.ToString(), true, out phoneNumberType)) {
						user.PhoneNumberType = phoneNumberType;
					}
					
					user.Addresses = new List<Address>();
					if(model.Addresses != null) {
						foreach(var address in model.Addresses) {
							// TODO: Check State value to Data integrity
							// TODO: Use a Binding Filter for AddressType
							user.Addresses.Add(address);							
						}
					}
					
					//TODO: Update the Profile via UserManager
					
					response.Data = user.CreateData();
					response.Description = "TODO: Update the User Profile via UserManager";			
					this.Context.Response.StatusCode = response.Code = 200;
				} else {
					response.Description = "Failed to retrieve your Profile information";
				}
			} catch(Exception ex) {
				response.Description = "Failed to retrieve me";					
				AppUtilityHelper.Logger.WriteError(response.Description, ex);
			}
		
			return  new JsonResult(response);
		} // end PostMe

	} // end class
} // end namespace