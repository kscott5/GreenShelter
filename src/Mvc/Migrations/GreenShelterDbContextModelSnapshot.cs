using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using PCSC.GreenShelter.Models;
using System;

namespace mvc.Migrations
{
    [ContextType(typeof(PCSC.GreenShelter.Models.GreenShelterDbContext))]
    public class GreenShelterDbContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder();
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityRoleClaim`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("ClaimType");
                        b.Property<string>("ClaimValue");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<int>("RoleId");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetRoleClaims");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityUserClaim`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("ClaimType");
                        b.Property<string>("ClaimValue");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<int>("UserId");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetUserClaims");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityUserLogin`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<string>("LoginProvider");
                        b.Property<string>("ProviderDisplayName");
                        b.Property<string>("ProviderKey");
                        b.Property<int>("UserId");
                        b.Key("LoginProvider", "ProviderKey");
                        b.ForRelational().Table("AspNetUserLogins");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityUserRole`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.Property<int>("RoleId");
                        b.Property<int>("UserId");
                        b.Key("UserId", "RoleId");
                        b.ForRelational().Table("AspNetUserRoles");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.Address", b =>
                    {
                        b.Property<int>("AddressType");
                        b.Property<int?>("ApplicationUserId");
                        b.Property<string>("City");
                        b.Property<string>("CountryCode");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("StateCode");
                        b.Property<string>("Street1");
                        b.Property<string>("Street2");
                        b.Property<string>("ZipCode");
                        b.Key("Id");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.ApplicationRole", b =>
                    {
                        b.Property<string>("ConcurrencyStamp")
                            .ConcurrencyToken();
                        b.Property<string>("Description");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("Name");
                        b.Property<string>("NormalizedName");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetRoles");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.ApplicationUser", b =>
                    {
                        b.Property<int>("AccessFailedCount");
                        b.Property<bool>("Active");
                        b.Property<string>("ConcurrencyStamp")
                            .ConcurrencyToken();
                        b.Property<DateTime>("CreationDate");
                        b.Property<string>("Email");
                        b.Property<bool>("EmailConfirmed");
                        b.Property<string>("FirstName");
                        b.Property<string>("GuidId");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<DateTime>("LastActive");
                        b.Property<string>("LastName");
                        b.Property<bool>("LockoutEnabled");
                        b.Property<DateTimeOffset?>("LockoutEnd");
                        b.Property<int?>("ModifiedByUserId");
                        b.Property<DateTime?>("ModifiedDate");
                        b.Property<string>("NormalizedEmail");
                        b.Property<string>("NormalizedUserName");
                        b.Property<string>("PasswordHash");
                        b.Property<string>("PhoneNumber");
                        b.Property<bool>("PhoneNumberConfirmed");
                        b.Property<int>("PhoneNumberType");
                        b.Property<int?>("RoleId");
                        b.Property<string>("SSNo");
                        b.Property<string>("SecurityStamp");
                        b.Property<bool>("TwoFactorEnabled");
                        b.Property<string>("UserName");
                        b.Key("Id");
                        b.ForRelational().Table("AspNetUsers");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.Organization", b =>
                    {
                        b.Property<int?>("ApplicationUserId");
                        b.Property<string>("ContactEmail1");
                        b.Property<string>("ContactEmail2");
                        b.Property<string>("ContactName1");
                        b.Property<string>("ContactName2");
                        b.Property<string>("ContactPhone1");
                        b.Property<string>("ContactPhone2");
                        b.Property<string>("EntityCode");
                        b.Property<int>("Id")
                            .GenerateValueOnAdd();
                        b.Property<string>("Name");
                        b.Property<string>("Phone");
                        b.Property<string>("SecretKey");
                        b.Property<string>("Url");
                        b.Key("Id");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityRoleClaim`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("PCSC.GreenShelter.Models.ApplicationRole", "RoleId");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityUserClaim`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("PCSC.GreenShelter.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("Microsoft.AspNet.Identity.IdentityUserLogin`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]", b =>
                    {
                        b.ForeignKey("PCSC.GreenShelter.Models.ApplicationUser", "UserId");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.Address", b =>
                    {
                        b.ForeignKey("PCSC.GreenShelter.Models.ApplicationUser", "ApplicationUserId");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.ApplicationUser", b =>
                    {
                        b.ForeignKey("PCSC.GreenShelter.Models.ApplicationRole", "RoleId");
                    });
                
                builder.Entity("PCSC.GreenShelter.Models.Organization", b =>
                    {
                        b.ForeignKey("PCSC.GreenShelter.Models.ApplicationUser", "ApplicationUserId");
                    });
                
                return builder.Model;
            }
        }
    }
}