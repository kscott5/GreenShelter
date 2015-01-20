using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.Logging;
using Xunit;

using PCSC.GreenShelter;
using PCSC.GreenShelter.Extensions;

namespace PCSC.GreenShelter.Extensions.Test {
	/// <summary>
	/// Test Extensions methods utilized by the Green Shelter application
	/// </summary>
	public class GreenShelterApplicationExtensionsTest {
		class App: IGreenShelterApplication {
			public string TagName {get { return "GreenShelterApplicationExtensionsTest"; } }
		}

		[Fact]
		public void IsLoggerInitialized() {
			// Arrange
			var app = new App();
            
            // Act
            var logger = app.Logger();

            // Assert
            Assert.NotNull(logger);
		}

		[Fact]
		public void IsConfigurationInitialized() {
			// Arrange
			var app = new App();
            
            // Act
            var configuration = app.Configuration();

            // Assert
            Assert.NotNull(configuration);
		}
		
		[Fact]
        public void AppName()
        {
			// Arrange
			var app = new App();
            
            // Act
            var appName = app.AppName();

            // Assert
            Assert.Equal("Green Shelter", appName);
        }


		[Fact]
		public void WriteInformation() {
			// TODO: Mock the logger.WriteInformation() method			
		}
	} // end class
} // end namespace