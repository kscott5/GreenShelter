using System;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

using PCSC.GreenShelter;

namespace PCSC.GreenShelter.Extensions {
	/// <summary>
	/// Extensions methods utilized by the Green Shelter application
	/// </summary>
	public static class GreenShelterApplicationExtensions {
		/// <summary>
		/// Returns the name of the application
		/// </summary>
		public static string ApplicationName(this IGreenShelterApplication app) {
			return AppUtilityHelper.Configuration.Get("Application:Name");
		}

		/// <summary>
		/// Returns the version of the application
		/// </summary>
		public static string ApplicationVersion(this IGreenShelterApplication app) {
			return AppUtilityHelper.Configuration.Get("Application:Version");
		}
		
		public static string DefaultAdminUserName(this IGreenShelterApplication app) {
			return AppUtilityHelper.Configuration.Get("DefaultAdminUserName");
		}
		
		public static string DefaultAdminPassword(this IGreenShelterApplication app) {
			return AppUtilityHelper.Configuration.Get("DefaultAdminPassword");
		}
	} // end class
} // end namespace