using Microsoft.AspNet.Mvc;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Controllers
{
	/// <summary>
	///
	/// </summary>
	public class HomeController : Controller, IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName {get { return "HomeController" ; } }

		public IActionResult Index() {
			this.WriteInformation("Index");
			var userMgr = Context.ApplicationServices.GetService(typeof(ApplicationUserManager)) as ApplicationUserManager;
			this.WriteInformation(string.Format("User Manager: {0}", userMgr));
			
            return View();
        }
		
		public IActionResult Error() {
			this.WriteInformation("Error");
			
			return View();
		}
		
	} // end class
} // end namespace