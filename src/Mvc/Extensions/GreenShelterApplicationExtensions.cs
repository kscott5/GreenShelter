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
		
		// Internal extension helper class
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
		}

		//end internal class

		/// <summary>
		/// Returns the application wide configuration
		/// </summary>
		public static IConfiguration Configuration(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration;
		}

		/// <summary>
		/// Determines if Facbook Authentication Services is enabled
		/// </summary>
		public static bool FacebookEnabled(this IGreenShelterApplication app) {
			bool result;
			bool.TryParse(ExtensionHelper.configuration.Get("Authentication:Facebook:Enabled"), out result);
			return result;
		}

		/// <summary>
		/// Gets the Facbook Authentication Services App Id
		/// </summary>
		public static string FacebookAppId(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:Facebook:AppId");
		}

		/// <summary>
		/// Gets the Facbook Authentication Services App Secret
		/// </summary>
		public static string FacebookAppSecret(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:Facebook:AppSecret");
		}

		/// <summary>
		/// Determines if Google Authentication Services is enabled
		/// </summary>
		public static bool GoogleEnabled(this IGreenShelterApplication app) {
			bool result;
			bool.TryParse(ExtensionHelper.configuration.Get("Authentication:Google:Enabled"), out result);
			return result;
		}

		/// <summary>
		/// Gets the Google Authentication Services Client Id
		/// </summary>
		public static string GoogleClientId(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:Google:ClientId");
		}

		/// <summary>
		/// Gets the Google Authentication Services Client Secret
		/// </summary>
		public static string GoogleClientSecret(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:Google:ClientSecret");
		}

		/// <summary>
		/// Determines if Microsoft Account Authentication Services is enabled
		/// </summary>
		public static bool MicrosoftAccountEnabled(this IGreenShelterApplication app) {
			bool result;
			bool.TryParse(ExtensionHelper.configuration.Get("Authentication:MicrosoftAccount:Enabled"), out result);
			return result;
		}

		/// <summary>
		/// Gets the Microsoft Account Authentication Services Caption
		/// </summary>
		public static string MicrosoftAccountCaption(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:MicrosoftAccount:Caption");
		}

		/// <summary>
		/// Gets the Microsoft Account Authentication Services Client Id
		/// </summary>
		public static string MicrosoftAccountClientId(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:MicrosoftAccount:ClientId");
		}

		/// <summary>
		/// Gets the Microsoft Account Authentication Services Client Secret
		/// </summary>
		public static string MicrosoftAccountClientSecret(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:MicrosoftAccount:ClientSecret");
		}

		/// <summary>
		/// Determines if Twitter Authentication Services is enabled
		/// </summary>
		public static bool TwitterEnabled(this IGreenShelterApplication app) {
			bool result;
			bool.TryParse(ExtensionHelper.configuration.Get("Authentication:Twitter:Enabled"), out result);
			return result;
		}

		/// <summary>
		/// Gets the Twitter Authentication Services Consumer Key
		/// </summary>
		public static string TwitterConsumerKey(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:Twitter:ConsumerKey");
		}

		/// <summary>
		/// Gets the Twitter Authentication Services Consumer Secret
		/// </summary>
		public static string TwitterConsumerSecret(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Authentication:Twitter:ConsumerSecret");
		}

		/// <summary>
		/// Gets connection string to the database
		/// </summary>
		public static string ConnectionString(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Data:DefaultConnection:ConnectionString");
		}

		/// <summary>
		/// Returns the name of the application
		/// </summary>
		public static string ApplicationName(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Application:Name");
		}

		/// <summary>
		/// Returns the version of the application
		/// </summary>
		public static string ApplicationVersion(this IGreenShelterApplication app) {
			return ExtensionHelper.configuration.Get("Application:Version");
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