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
		private static string APP_NAME = "Green Shelter";
		
		/// Internal extension helper class
		internal class ExtensionHelper {
			public static IConfiguration configuration;
			public static ILogger logger;

			static ExtensionHelper() {
				// Initialize static member variables
				if(logger == null) {
					var factory = new LoggerFactory();

					logger = factory.Create(GreenShelterApplicationExtensions.APP_NAME);
					
					#if !ASPNETCORE50
						factory.AddNLog(new global::NLog.LogFactory());
					#endif
					
					factory.AddConsole();
					factory.AddConsole((category, logLevel) => logLevel >= LogLevel.Critical && category.Equals("GreenShelter"));
				}
				
				if(configuration == null) {
					// Below code demonstrates usage of multiple configuration sources. For instance a setting say 'setting1' is 
					// found in both the registered sources, then the later source will win. By this way a Local config can be 
					// overridden by a different setting while deployed remotely.
					configuration = new Configuration()
					.AddJsonFile("config.json") // Currently not in master branch
					.AddEnvironmentVariables(); //All environment variables in the process's context flow in as configuration values.
				}
			}
		} //end class
		
		/// <summary>
		/// Returns the application wide configuration
		/// </summary>
		public static IConfiguration Configuration(this IGreenShelterApplication app) { 
			return ExtensionHelper.configuration;
		}

		public static string ConnectionString(this IGreenShelterApplication app) {
			var connString = ExtensionHelper.configuration.Get("development:databases:mongodb:connectionstring");
			return connString;
		}
		
		public static string DatabaseName(this IGreenShelterApplication app) {
			var connString = ExtensionHelper.configuration.Get("development:databases:mongodb:databasename");
			return connString;
		}
		
		/// <summary>
		/// Returns the name of the application
		/// </summary>
		public static string AppName(this IGreenShelterApplication app) {
			return GreenShelterApplicationExtensions.APP_NAME;
		}
		
		/// <summary>
		/// Returns the application wide logger
		/// </summary>
		public static ILogger Logger(this IGreenShelterApplication app) {
			return ExtensionHelper.logger; 
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void WriteInformation(this IGreenShelterApplication app, string message) {
			ExtensionHelper.logger.WriteInformation(string.Format("{0} => {1}", app.TagName, message));
		}
		
		/// <summary>
		/// 
		/// </summary>
		public static void WriteWarning(this IGreenShelterApplication app, string message, Exception exception) {
			ExtensionHelper.logger.WriteWarning(message, exception);
		}
		/// <summary>
		/// 
		/// </summary>
		public static void WriteError(this IGreenShelterApplication app, string message, Exception exception) {
			ExtensionHelper.logger.WriteError(message, exception);
		}
		
				/// <summary>
		/// 
		/// </summary>
		public static void WriteCritical(this IGreenShelterApplication app, string message, Exception exception) {
			ExtensionHelper.logger.WriteCritical(message, exception);
		}

	} // end class
} // end namespace