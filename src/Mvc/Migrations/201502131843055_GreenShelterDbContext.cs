using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.MigrationsModel;
using System;

namespace mvc.Migrations
{
    public partial class GreenShelterDbContext : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("AspNetRoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        RoleId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_AspNetRoleClaims", t => t.Id);
            
            migrationBuilder.CreateTable("AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_AspNetUserClaims", t => t.Id);
            
            migrationBuilder.CreateTable("AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ProviderDisplayName = c.String(),
                        UserId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_AspNetUserLogins", t => new { t.LoginProvider, t.ProviderKey });
            
            migrationBuilder.CreateTable("AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false)
                    })
                .PrimaryKey("PK_AspNetUserRoles", t => new { t.UserId, t.RoleId });
            
            migrationBuilder.CreateTable("Address",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressType = c.Int(nullable: false),
                        City = c.String(),
                        CountryCode = c.String(),
                        StateCode = c.String(),
                        Street1 = c.String(),
                        Street2 = c.String(),
                        ZipCode = c.String(),
                        ApplicationUserId = c.Int()
                    })
                .PrimaryKey("PK_Address", t => t.Id);
            
            migrationBuilder.CreateTable("AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConcurrencyStamp = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        NormalizedName = c.String()
                    })
                .PrimaryKey("PK_AspNetRoles", t => t.Id);
            
            migrationBuilder.CreateTable("AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccessFailedCount = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ConcurrencyStamp = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        LastActive = c.DateTime(nullable: false),
                        LockoutEnabled = c.Boolean(nullable: false),
                        LockoutEnd = c.DateTimeOffset(),
                        ModifiedByUserId = c.Int(),
                        ModifiedDate = c.DateTime(),
                        NormalizedEmail = c.String(),
                        NormalizedUserName = c.String(),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(),
                        SSNo = c.String(),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        UserName = c.String(),
                        RoleId = c.Int()
                    })
                .PrimaryKey("PK_AspNetUsers", t => t.Id);
            
            migrationBuilder.CreateTable("Organization",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactEmail1 = c.String(),
                        ContactEmail2 = c.String(),
                        ContactName1 = c.String(),
                        ContactName2 = c.String(),
                        ContactPhone1 = c.String(),
                        ContactPhone2 = c.String(),
                        EntityCode = c.String(),
                        Name = c.String(),
                        Phone = c.String(),
                        SecretKey = c.String(),
                        Url = c.String(),
                        ApplicationUserId = c.Int()
                    })
                .PrimaryKey("PK_Organization", t => t.Id);
            
            migrationBuilder.AddForeignKey(
                "AspNetRoleClaims",
                "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                new[] { "RoleId" },
                "AspNetRoles",
                new[] { "Id" },
                cascadeDelete: false);
            
            migrationBuilder.AddForeignKey(
                "AspNetUserClaims",
                "FK_AspNetUserClaims_AspNetUsers_UserId",
                new[] { "UserId" },
                "AspNetUsers",
                new[] { "Id" },
                cascadeDelete: false);
            
            migrationBuilder.AddForeignKey(
                "AspNetUserLogins",
                "FK_AspNetUserLogins_AspNetUsers_UserId",
                new[] { "UserId" },
                "AspNetUsers",
                new[] { "Id" },
                cascadeDelete: false);
            
            migrationBuilder.AddForeignKey(
                "Address",
                "FK_Address_AspNetUsers_ApplicationUserId",
                new[] { "ApplicationUserId" },
                "AspNetUsers",
                new[] { "Id" },
                cascadeDelete: false);
            
            migrationBuilder.AddForeignKey(
                "AspNetUsers",
                "FK_AspNetUsers_AspNetRoles_RoleId",
                new[] { "RoleId" },
                "AspNetRoles",
                new[] { "Id" },
                cascadeDelete: false);
            
            migrationBuilder.AddForeignKey(
                "Organization",
                "FK_Organization_AspNetUsers_ApplicationUserId",
                new[] { "ApplicationUserId" },
                "AspNetUsers",
                new[] { "Id" },
                cascadeDelete: false);
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey("AspNetRoleClaims", "FK_AspNetRoleClaims_AspNetRoles_RoleId");
            
            migrationBuilder.DropForeignKey("AspNetUsers", "FK_AspNetUsers_AspNetRoles_RoleId");
            
            migrationBuilder.DropForeignKey("AspNetUserClaims", "FK_AspNetUserClaims_AspNetUsers_UserId");
            
            migrationBuilder.DropForeignKey("AspNetUserLogins", "FK_AspNetUserLogins_AspNetUsers_UserId");
            
            migrationBuilder.DropForeignKey("Address", "FK_Address_AspNetUsers_ApplicationUserId");
            
            migrationBuilder.DropForeignKey("Organization", "FK_Organization_AspNetUsers_ApplicationUserId");
            
            migrationBuilder.DropTable("AspNetRoleClaims");
            
            migrationBuilder.DropTable("AspNetUserClaims");
            
            migrationBuilder.DropTable("AspNetUserLogins");
            
            migrationBuilder.DropTable("AspNetUserRoles");
            
            migrationBuilder.DropTable("Address");
            
            migrationBuilder.DropTable("AspNetRoles");
            
            migrationBuilder.DropTable("AspNetUsers");
            
            migrationBuilder.DropTable("Organization");
        }
    }
}