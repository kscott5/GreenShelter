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
		/// Gets connection string to the database
		/// </summary>
		public static string ConnectionString(this IGreenShelterApplication app) {
			var kre_env = AppUtilityHelper.Configuration.Get("KRE_ENV")?? "Development";
			var key = string.Format("Data:{0}:ConnectionString", kre_env);
			return AppUtilityHelper.Configuration.Get(key);
		}

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
		
		/// <summary>
		/// Returns the application wide logger
		/// </summary>
		public static ILogger Logger(this IGreenShelterApplication app) {
			return AppUtilityHelper.Logger; 
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void WriteInformation(this IGreenShelterApplication app, string message) {
			AppUtilityHelper.Logger.WriteInformation(string.Format("{0} => {1}", app.TagName, message));
		}
		
		public static void WriteInformation(this IGreenShelterApplication app, string format, params object[] args) {
			AppUtilityHelper.Logger.WriteInformation(format, args);
		}

		/// <summary>
		/// 
		/// </summary>
		public static void WriteWarning(this IGreenShelterApplication app, string message, Exception exception) {
			AppUtilityHelper.Logger.WriteWarning(message, exception);
		}
		/// <summary>
		/// 
		/// </summary>
		public static void WriteError(this IGreenShelterApplication app, string message, Exception exception) {
			AppUtilityHelper.Logger.WriteError(message, exception);
		}
		
		public static void WriteError(this IGreenShelterApplication app, string format, params object[] args) {
			AppUtilityHelper.Logger.WriteError(format, args);
		}

		/// <summary>
		/// 
		/// </summary>
		public static void WriteCritical(this IGreenShelterApplication app, string message, Exception exception) {
			AppUtilityHelper.Logger.WriteCritical(message, exception);
		}

		public static void WriteCritical(this IGreenShelterApplication app, string format, params object[] args) {
			AppUtilityHelper.Logger.WriteCritical(format, args);
		}
	} // end class
} // end namespace