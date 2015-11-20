using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using PCSC.GreenShelter.Models;

namespace mvc.Migrations
{
    [DbContext(typeof(GreenShelterDbContext))]
    partial class GreenShelterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta8-15964");

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.Annotation("Relational:TableName", "AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.Annotation("Relational:TableName", "AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.Annotation("Relational:TableName", "AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.Annotation("Relational:TableName", "AspNetUserRoles");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "AddressId");

                    b.Property<int>("AddressType");

                    b.Property<int?>("ApplicationUserId");

                    b.Property<string>("City");

                    b.Property<string>("CountryCode");

                    b.Property<string>("StateCode");

                    b.Property<string>("Street1");

                    b.Property<string>("Street2");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .Annotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.Index("NormalizedName")
                        .Annotation("Relational:Name", "RoleNameIndex");

                    b.Annotation("Relational:TableName", "AspNetRoles");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<bool>("Active");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Email")
                        .Annotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("GuidId");

                    b.Property<DateTime>("LastActive");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int?>("ModifiedByUserId");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("NormalizedEmail")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PhoneNumberType");

                    b.Property<int?>("RoleId");

                    b.Property<string>("SSNo")
                        .Annotation("MaxLength", 9);

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .Annotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.Index("NormalizedEmail")
                        .Annotation("Relational:Name", "EmailIndex");

                    b.Index("NormalizedUserName")
                        .Annotation("Relational:Name", "UserNameIndex");

                    b.Annotation("Relational:TableName", "AspNetUsers");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .Annotation("Relational:ColumnName", "OrganizationId");

                    b.Property<int?>("ApplicationUserId");

                    b.Property<string>("ContactEmail1");

                    b.Property<string>("ContactEmail2");

                    b.Property<string>("ContactName1");

                    b.Property<string>("ContactName2");

                    b.Property<string>("ContactPhone1");

                    b.Property<string>("ContactPhone2");

                    b.Property<string>("EntityCode");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("SecretKey");

                    b.Property<string>("Url");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationRole")
                        .WithMany()
                        .ForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationRole")
                        .WithMany()
                        .ForeignKey("RoleId");

                    b.HasOne("PCSC.GreenShelter.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("UserId");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.Address", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.ApplicationUser", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationRole")
                        .WithMany()
                        .ForeignKey("RoleId");
                });

            modelBuilder.Entity("PCSC.GreenShelter.Models.Organization", b =>
                {
                    b.HasOne("PCSC.GreenShelter.Models.ApplicationUser")
                        .WithMany()
                        .ForeignKey("ApplicationUserId");
                });
        }
    }
}
