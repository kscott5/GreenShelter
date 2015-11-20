﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

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
			return View();
		}

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

	} // end class
} // end namespace