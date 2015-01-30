using System.ComponentModel.DataAnnotations;

namespace PCSC.GreenShelter.Models {
	public class ApiResponse {
		public int Code { get; set; }
		public string Description { get; set; }
		public object Data { get; set; }
	} // end class
} // end namespace