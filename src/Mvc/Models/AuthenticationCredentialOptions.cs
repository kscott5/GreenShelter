namespace PCSC.GreenShelter.Models {
	/// <summary>
	///  Authentication Credentials for External Authentication Providers
	/// </summary>
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