using System;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Api.v1
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

	} // end class
} // end namespace