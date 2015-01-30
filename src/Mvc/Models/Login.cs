using System.ComponentModel.DataAnnotations;

namespace PCSC.GreenShelter.Models {
	public class Login {
		[Required]
		[EmailAddress]
		public string Email {get; set;}
		
		[Required]
		[DataType("Password")]
		public string Password {get; set;}
		
		public bool RememberMe {get; set;}
	}
} // end namespace