﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PCSC.GreenShelter.Models {
	public enum PhoneNumberType : int {
		Home = 0,
		Mobile = 1,
		Other = 2
	};

	// DataAnnotations are not support in EF 7. So here's the alternative cause I 
	// still need to remove duplication found in the Login.cs and Registration.cs 
	// classes used for client/server model data-binding
	// https://github.com/aspnet/EntityFramework/issues/1763#issuecomment-77431815
	
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
	[Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser<int>   {
		public ApplicationUser() : base() {
			SSNo = String.Empty;
            GuidId = Guid.NewGuid();
			Addresses = new List<Address>();
			Organizations = new List<Organization>();
		}

        /// <summary>
        /// Password is minimum 8 characters at least 1 Uppercase Alphabet, 1 Lowercase Alphabet, 1 Number and 1 Special Character
        /// </summary>
        [Required]       
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)(?=.*[$@$!%*#?&])[A-Za-z\\d$@$!%*#?&]{8,}$")] 
        public override string PasswordHash {get; set; }

        /// <summary>
        /// https://html.spec.whatwg.org/multipage/forms.html#valid-e-mail-address
        /// </summary>        
        [Required]
        [RegularExpression("^[a-zA-Z0-9.!#$%&'*+\\/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$")]
        public override string Email {get; set;}
        
		[NotMapped]
		[Compare("PasswordHash")]
		public virtual string ConfirmedPassword {get; set;}
		
		[Key]
		[Column("UserId")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public override int Id {get; set;}
		
		public virtual Guid GuidId {get; set;}
		
        [Column("RoleId")]
        [ForeignKey("RoleId")]
        public virtual ApplicationRole Role {get; set;}
		
        public virtual string FirstName {get; set; }
		public virtual string LastName {get; set; }
		
		public virtual List<Address> Addresses {get; set;}
		public virtual List<Organization> Organizations {get; set;}
		
		public virtual PhoneNumberType PhoneNumberType {get; set;}
		
		[StringLength(9)]
		public virtual string SSNo { get; set; }
		public virtual bool Active {get; set;}
		public virtual DateTime LastActive {get; set;}
		
		public virtual DateTime CreationDate {get; set;}
		
		public virtual DateTime? ModifiedDate {get; set;}
		
		public virtual ApplicationUser ModifiedByUser {get; set;}		
		
		/// <summary>
		/// Creates a data object used for JsonResult serialization. This
		/// ensures only the fields required by the client-side are made available.
		/// </summary>
		public virtual object CreateData(bool isAuthenticated = false, string returnUrl = null) {
			return new {
				GuidId = this.GuidId,
				FirstName = this.FirstName,
				LastName = this.LastName,
				
				PhoneNumber = this.PhoneNumber,
				PhoneNumberType = (int)this.PhoneNumberType,
				
				Addresses = this.Addresses,
				Organizations = this.Organizations,
				
				IsAuthenticated = isAuthenticated,
				ReturnUrl = returnUrl
			};
		}
		
		public override string ToString() {
			StringBuilder builder = new StringBuilder();
			
			builder.AppendFormat("User Id: {0}\n", this.Id);			
			builder.AppendFormat("User Name: {0}\n", this.UserName);
			builder.AppendFormat("Email: {0}\n", this.Email);
			builder.AppendFormat("Full Name: {0} {1}\n", this.FirstName, this.LastName);
			//builder.AppendFormat("SSNo: xxx-xxx-{0}\n", this.SSNo.Substring(5));
			
			return builder.ToString();
		}
    } // end class
} // end namespace