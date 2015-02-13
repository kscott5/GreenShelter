using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PCSC.GreenShelter.Models {
	public class Login {
		[Required]
		[EmailAddress]
		public string UserName {get; set;}
		
		[Required]
		[DataType("Password")]
		public string Password {get; set;}		
	}
} // end namespace