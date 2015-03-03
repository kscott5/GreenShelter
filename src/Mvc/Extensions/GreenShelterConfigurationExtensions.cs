using System;
using Microsoft.Framework.ConfigurationModel;

using PCSC.GreenShelter;

namespace PCSC.GreenShelter.Extensions {
	/// <summary>
	/// Extensions methods for Configuration
	/// </summary>
	public static class GreenShelterConfigurationExtensions {
		/// <summary>
		/// Gets connection string to the database
		/// </summary>
		public static string ConnectionString(this IConfiguration config) {
			var kre_env = config.Get("KRE_ENV")?? "Development";
			var key = string.Format("Data:{0}:ConnectionString", kre_env);
			return config.Get(key);
		}

		/// <summary>
		/// Returns the name of the application
		/// </summary>
		public static string ApplicationName(this IConfiguration config) {
			return config.Get("Application:Name");
		}

		/// <summary>
		/// Returns the version of the application version
		/// </summary>
		public static string ApplicationVersion(this IConfiguration config) {
			return config.Get("Application:Version");
		}
		
		public static string DefaultAdminUserName(this IConfiguration config) {
			return config.Get("DefaultAdminUserName");
		}
		
		public static string DefaultAdminPassword(this IConfiguration config) {
			return config.Get("DefaultAdminPassword");
		}
	} // end class
} // end namespace