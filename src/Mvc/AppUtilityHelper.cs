using System;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace PCSC.GreenShelter {
	internal class AppUtilityHelper {
		
		public static IConfiguration Configuration {get; private set;} 
		public static ILogger Logger {get; private set;}

		static AppUtilityHelper() {
			var factory = new LoggerFactory();

			AppUtilityHelper.Logger = factory.Create("GreenShelterLogger");

			#if !ASPNETCORE50
				factory.AddNLog(new global::NLog.LogFactory());
			#endif

			factory.AddConsole();
			factory.AddConsole((category, logLevel) => logLevel >= 0);

			// Below code demonstrates usage of multiple configuration sources. For instance a setting say 'setting1' is 
			// found in both the registered sources, then the later source will win. By this way a Local config can be 
			// overridden by a different setting while deployed remotely.
			AppUtilityHelper.Configuration = new Configuration()
			.AddJsonFile("config.json") // Currently not in master branch
			.AddEnvironmentVariables(); //All environment variables in the process's context flow in as configuration values.
		}
	} //end internal class
} // end namespace