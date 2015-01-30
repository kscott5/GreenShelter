using System.ComponentModel.DataAnnotations;

namespace PCSC.GreenShelter.Models {
	public class Registration {
		[Required]
		[EmailAddress]
		public string Email {get; set;}
		
		[Required]
		[DataType("Password")]
		public string Password {get; set;}
		
		[Required]
		[Compare("Password")]
		public string ConfirmPassword {get; set;}
		
		public bool RememberMe {get; set;}
	}
} // end namespace