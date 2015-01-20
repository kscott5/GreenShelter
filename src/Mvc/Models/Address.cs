
namespace PCSC.GreenShelter.Models {
	public class Address {
		public virtual string Street1 {get; set;}
		public virtual string Street2 {get; set;}
		public virtual string City {get; set;}
		public virtual string StateCode {get; set;}
		public virtual string ZipCode {get; set;}
		public virtual string CountryCode {get; set;}
		public virtual string AddressType {get; set;}
	}
}