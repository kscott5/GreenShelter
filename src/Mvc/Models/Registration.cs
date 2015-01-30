using System.ComponentModel.DataAnnotations;

namespace PCSC.GreenShelter.Models {
	public class Registration {
		[Required]
		/// <summary>
		/// Possibly email address or ss#
		/// </summary>
		public string UserName {get; set;}
		
		[Required]
		[DataType("Text")]
		public string Password {get; set;} // TODO: Regular Expression to set min/max length and format
		
		[Required]
		[Compare("Password")]
		public string ConfirmPassword {get; set;}

		[Required]
		[StringLength(9)]
		public string SSNo {get; set;}
	}
} // end namespace