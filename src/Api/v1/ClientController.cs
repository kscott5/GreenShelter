using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Api.v1 {
    //[Authorize]
	[Route("api/v1/client", Name = "Client")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class ClientController : Controller, IGreenShelterApplication {
		
		public ClientController(ILogger<ClientController> logger, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) {
           SignInManager = signInManager;
		   UserManager = userManager;
		   Logger = logger;
        }

		protected ILogger Logger { get; private set; }
        protected SignInManager<ApplicationUser> SignInManager { get; private set; }
		protected UserManager<ApplicationUser> UserManager {get; private set;}
		
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "ClientController" ; } }

		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
        [Route("ExternalLogin", Name="ExternalLogin")]
        public IActionResult ExternalLogin(string AuthenticationType, string returnUrl = null) {		
            throw new NotImplementedException();
        }

        [HttpGet]
        [AllowAnonymous]
		[Route("ExternalLoginCallback", Name = "ExternalLoginCallback")]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null) {
            throw new NotImplementedException();
        }

		[HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
		[Route("ExternalLoginConfirmation", Name="ExternalLoginConfirmation")]
        public async Task<IActionResult> ExternalLoginConfirmation(string Email, string returnUrl = null)
        {
            throw new NotImplementedException();
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
			this.Logger.LogInformation("AuthTypes");
			return new JsonResult("{}");
        }
		
		[HttpPost]
		//[AllowAnonymous]
		[ValidateAntiForgeryToken]
		[Route("Login", Name="Login")]
		public async Task<JsonResult> Login([FromBody] ApplicationUser model, string returnUrl = null) {
			throw new NotImplementedException();
		}
		
		[HttpPost]
		[AllowAnonymous]
		//[ValidateAntiForgeryToken]
		[Route("Register", Name="Register")]
		public async Task<JsonResult> Register([FromBody] ApplicationUser model) {
			this.Logger.LogInformation("Register: {0}", model);
			
			var response = new ApiResponse { Code = 400	};
			
			try {
				var result = await this.UserManager.CreateAsync(model);
				if(result.Succeeded) {
					await this.SignInManager.SignInAsync(model, false);

					response.Code = 200;
					response.Data =  model.CreateData(true);
					response.Description = string.Format("{0} registered successfully", model.UserName);
				} else {
					response.Description = string.Format("{0} Registration failed", model.UserName);
					response.Data = result.Errors;
				}
			} catch(Exception ex) {
				response.Description = string.Format("Failed to register new User {0}", model.UserName);
				this.Logger.LogError(response.Description, ex);
			}
			
			this.HttpContext.Response.StatusCode = response.Code;
						
			return new JsonResult(response);
		}
		
		[Route("Me/{guidId?}", Name="Me")]
		public async Task<JsonResult> Me(string guidId, [FromBody] ApplicationUser model = null) {
			throw new NotImplementedException();
		} // end Me
		
		private async Task<JsonResult> GetMe(string guidId) {
			throw new NotImplementedException();
		} // end GetMe
		
		private async Task<JsonResult> PostMe(string guidId, ApplicationUser model) {
			throw new NotImplementedException();
		} // end PostMe

	} // end class
} // end namespace