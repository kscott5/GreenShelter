using System;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Controllers.Api.V1
{
	/// <summary>
	///
	/// </summary>
	[Route("api/v1/shelter")]
	public class ShelterController : Controller, IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "Api/v1/ShelterController"; } }

		/// <summary>
		///
		/// </summary>
		[HttpGet("ok")]
		public IActionResult GetOk() {
			this.WriteInformation("GetOK");

			try {
				var antiForgery = Context.ApplicationServices.GetService(typeof(AntiForgery)) as AntiForgery;
				antiForgery.SetCookieTokenAndHeader(Context);
				
			} catch(Exception ex) {
				this.WriteError("Failed get antiforgery", ex);
			}


			return new ObjectResult(string.Format("OK: {0}", "Get token"));
		}
		
		/// <summary>
		///
		/// </summary>
		[HttpGet("client")]
		public IActionResult GetClient() {
			this.WriteInformation("GetClient");
			
           return new ObjectResult("Client OK");
        }
		
		/// <summary>
		///
		/// </summary>
		[HttpGet("client/{id}")]
		public IActionResult GetClientById(int id) {
			this.WriteInformation("GetClientById");
			
			return new ObjectResult(string.Format("Client Id: {0}", id));
		}
		
		/// <summary>
		///
		/// </summary>
		/// <remarks>
		/// Remember to use url encoded for the content type and
		//		Content-Type: application/x-www-form-urlencoded
		/// form a querystring url in the request body
		// 		first=karega&last=scott
		// </remarks>
		[HttpPost("client/new")]
		public JsonResult CreateNewClient(string first, string last) {
			this.WriteInformation("CreateNewClient");
			
			return new JsonResult(new {
				FirstName = first,
				LastName = last
			});
		}
	} // end class
} // end namespace