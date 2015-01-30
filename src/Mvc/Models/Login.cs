using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PCSC.GreenShelter.Models {
	public class Login {
		[Required]
		/// <summary>
		/// Possibly email address or ss#
		/// </summary>
		public string UserName {get; set;}
		
		[Required]
		[DataType("Password")]
		public string Password {get; set;}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			
			int result;
			builder.Append("UserName: ");
			if(int.TryParse(UserName, out result)) {
				builder.AppendLine("social security number");
			} else {
				builder.AppendLine(this.UserName);
			}
			
			builder.AppendLine("Password: **********");
			
			return builder.ToString();
		}
	}
} // end namespace