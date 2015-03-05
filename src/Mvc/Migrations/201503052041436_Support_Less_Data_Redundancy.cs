using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.MigrationsModel;
using System;

namespace mvc.Migrations
{
    public partial class Support_Less_Data_Redundancy : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn("Address", "AddressType", c => c.Int(nullable: false));
            
            migrationBuilder.AlterColumn("AspNetUsers", "PhoneNumberType", c => c.Int(nullable: false));
            
            migrationBuilder.AddColumn("AspNetUsers", "ConfirmedPassword", c => c.String());
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("AspNetUsers", "ConfirmedPassword");
            
            migrationBuilder.AlterColumn("Address", "AddressType", c => c.Int(nullable: false));
            
            migrationBuilder.AlterColumn("AspNetUsers", "PhoneNumberType", c => c.Int(nullable: false));
        }
    }
}