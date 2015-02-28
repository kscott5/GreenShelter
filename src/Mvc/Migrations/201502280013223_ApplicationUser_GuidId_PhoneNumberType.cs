using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.MigrationsModel;
using System;

namespace mvc.Migrations
{
    public partial class ApplicationUser_GuidId_PhoneNumberType : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn("Address", "AddressType", c => c.Int(nullable: false));
            
            migrationBuilder.AddColumn("AspNetUsers", "GuidId", c => c.String());
            
            migrationBuilder.AddColumn("AspNetUsers", "PhoneNumberType", c => c.Int(nullable: false));
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("AspNetUsers", "GuidId");
            
            migrationBuilder.DropColumn("AspNetUsers", "PhoneNumberType");
            
            migrationBuilder.AlterColumn("Address", "AddressType", c => c.Int(nullable: false));
        }
    }
}