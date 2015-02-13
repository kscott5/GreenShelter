using System;
using Microsoft.AspNet.Mvc;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;
using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter.Controllers
{
	/// <summary>
	///
	/// </summary>
	public class SpaController : Controller, IGreenShelterApplication
	{
		/// <summary>
		/// Describe name for the class implementing <cref="IGreenShelterApplication"/> interface
		/// </summary>
		public string TagName { get { return "SpaController"; } }

		public IActionResult StartPage() {
			this.WriteInformation("StartPage");

			return View();
		}

	} // end class
} // end namespace