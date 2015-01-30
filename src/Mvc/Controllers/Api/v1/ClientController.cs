using System;
using System.Collections.Generic;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Controllers.Api.V1 {
    [Authorize]
	[Route("api/v1/client")]
    public class ClientController : Controller, IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "ClientController" ; } }

        [HttpGet("authtypes")]
        [AllowAnonymous]
        public JsonResult AuthenticationTypes() {
			this.WriteInformation("/api/v1/client/authtypes");
			
			var response = new ApiResponse();
			
			try {
				response.Code = 200;
				response.Description = "Retreiving authtypes was successful";
				//var manager = Context.ApplicationServices.GetService(typeof(SignInManager));
				//response.Data = manager.GetExternalAuthenticationTypes().ToList();				
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Retreiving authtypes was unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
        }
		
		[HttpPost("login")]
		[AllowAnonymous]
		public JsonResult Login([FromBody] Login login) {
			this.WriteInformation("/api/v1/client/login [POST] login: {0}", login);
			
			var response = new ApiResponse();
			
			try {
				response.Code = 200;
				response.Description = "Login was successful";			
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Login was unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
		}
		
		[HttpPost("register")]
		[AllowAnonymous]
		public JsonResult Register([FromBody] Registration registration) {
			return new JsonResult(new ApiResponse());
		}
		
		[HttpGet("me")]
		public JsonResult Me(string id) {
			this.WriteInformation("/api/v1/client/me id: {0}", id);
			
			var response = new ApiResponse {
				Data = id
			};
			
			try {
				response.Code = 200;
				response.Description = "Retreiving me was successful";			
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Retreiving me was unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
		}
		
		[HttpPost("me")]
		public JsonResult Me([FromBody] ApplicationUser user) {
			this.WriteInformation("/api/v1/client/me [POST] email: {0}", user.Email);
			
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