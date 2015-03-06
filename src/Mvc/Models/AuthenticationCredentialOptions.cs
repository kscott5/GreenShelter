namespace PCSC.GreenShelter.Models {
	/// <summary>
	///  Authentication Credentials for External Authentication Providers.
	/// </summary>
	/// <remarks>
	/// This class is create by reading the AuthenticationCredentials section 
	//  contained in JSON configuration file for your application or config.json.
	/// Below is an example of the AuthenticationCredentials section.
	///
	///	"AuthenticationCredentials": {
	///		"Facebook": { 
	///			"Enabled": "false",
	///			"AppId": "",
	///			"AppSecret": ""
	///		},
	///		"Google": { /* https://console.developers.google.com */
	///			"Enabled": "true",
	///			"ClientId": "204805279074-lbqt4u5iq7uqjbthdkqb1b8sgqsl3ugc.apps.googleusercontent.com",
	///			"ClientSecret": "pDfENoR3XWlsw0VFOukYdmwG",
	///			"CallbackPath": "/api/v1/client/externalcallback",
	///			"Scope": "profile,email"
	///		},
	///		"MicrosoftAccount": { /* https://account.live.com/developers/applications/index */
	///			"Enabled": "false",
	///			"ClientId": "",
	///			"ClientSecret": ""
	///		},
	///		"Twitter": { /* https://dev.twitter.com/rest/tools/console, https://apps.twitter.com/ */
	///			"Enabled": "false",
	///			"ConsumerKey": "",
	///			"ConsumerSecret": ""
	///		}		
	///	}
	///
	/// Use ConfigureGoogleAuthentication and UseGoogleAuthentication extension methods located in
	///	the <see cref="GreenShelterGoogleProviderExtensions"/> class. These methods can be accessed
	/// during the application's start-up ConfigureService and Configure methods located in 
	//	<see cref="Startup"/>.
	/// </remarks>
	public class AuthenticationCredentialOptions {	
		public static readonly string Key = "AuthenticationCredentials";
		
		public AuthenticationCredentialOptions() {
			Google = new GoogleAuthenticationCredentialOptions();
			Facebook = new FacebookAuthenticationCredentialOptions();
			MicrosoftAccount = new MicrosoftAccountAuthenticationCredentialOptions();
			Twitter = new TwitterAuthenticationCredentialOptions();
		}
		
		public GoogleAuthenticationCredentialOptions Google {get; set;}
		public FacebookAuthenticationCredentialOptions Facebook {get; set;}
		public MicrosoftAccountAuthenticationCredentialOptions MicrosoftAccount {get; set;}
		public TwitterAuthenticationCredentialOptions Twitter {get; set;}
	} // end class

	public class GoogleAuthenticationCredentialOptions {
		public static readonly string Key = "Google";
		
		public bool Enabled {get;set;} 
		public string ClientId {get; set;}
		public string ClientSecret {get; set;}
		public string CallbackPath {get; set;}
		public string Scope {get; set; }
	} // end class
	
	public class FacebookAuthenticationCredentialOptions {
		public static readonly string Key = "Facebook";
		public bool Enabled {get;set;} 
		public string AppId {get; set;}
		public string AppSecret {get; set;}
	} // end class

	public class MicrosoftAccountAuthenticationCredentialOptions {
		public static readonly string Key = "MicrosoftAccount";
		
		public bool Enabled {get;set;} 
		public string ClientId {get; set;}
		public string ClientSecret {get; set;}
	} // end class
	
	public class TwitterAuthenticationCredentialOptions {
		public static readonly string Key = "Twitter";
		
		public bool Enabled {get;set;} 
		public string ConsumerId {get; set;}
		public string ConsumerSecret {get; set;}
	} // end class
} // end namespace