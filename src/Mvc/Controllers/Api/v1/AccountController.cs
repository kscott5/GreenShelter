using System;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Controllers.Api.V1 {
    [Authorize]
	[Route("api/v1/account")]
    public class AccountController : Controller, IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "AccountController" ; } }

        [HttpGet("login")]
        [AllowAnonymous]
		[Produces("application/json")]
        public JsonResult Login(string returnUrl) {
			var login = new Login();
			
			return new JsonResult(login);
        }
		
		[HttpPost("login")]
		[AllowAnonymous]
		public JsonResult Login([FromBody] Login login) {
			this.WriteInformation("Login [POST] email: {0}, password: ******", login.Email);
			
			var response = new ApiResponse {
				Data = login
			};
			
			try {
				response.Code = 200;
				response.Description = "Login Successful";			
			} catch(Exception ex) {
				response.Code = 401;
				response.Description = "Login unsuccessful";
				
				this.WriteError(response.Description, ex);
			}
			
			return  new JsonResult(response);
		}
	} // end class
} // end namespace