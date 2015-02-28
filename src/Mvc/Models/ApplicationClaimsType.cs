namespace PCSC.GreenShelter.Models {	
	public static class ApplicationClaimsType {
		public static readonly string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
		public static readonly string UserId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
		public static readonly string GuidId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/guididentifier";
		
		// TODO: Remove these below...
		//
		// I probably shouldn't save these objects below as a claim. If they are allow to change these objects
		public static readonly string UserName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
		public static readonly string Password = "http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod";
	} // end class
} // end namespace