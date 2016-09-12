using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCSC.GreenShelter.Models {
	public class Organization {
		[Key]
		[Column("OrganizationId")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual int Id {get; set;}
		public virtual string SecretKey {get; set;}
		
		public virtual string Name {get; set;}
		public virtual string EntityCode {get; set;}
		public virtual string Url {get; set;}
		
		public virtual string Phone {get; set;}
		
		public virtual string ContactName1 {get; set;}
		public virtual string ContactPhone1 {get; set;}
		public virtual string ContactEmail1 {get; set;}
		
		public virtual string ContactName2 {get; set;}
		public virtual string ContactPhone2 {get; set;}
		public virtual string ContactEmail2 {get; set;}
	}
}