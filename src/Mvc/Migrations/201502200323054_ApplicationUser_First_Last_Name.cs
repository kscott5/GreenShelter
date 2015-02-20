using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.MigrationsModel;
using System;

namespace mvc.Migrations
{
    public partial class ApplicationUser_First_Last_Name : Migration
    {
        public override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn("Address", "AddressType", c => c.Int(nullable: false));
            
            migrationBuilder.AddColumn("AspNetUsers", "FirstName", c => c.String());
            
            migrationBuilder.AddColumn("AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("AspNetUsers", "FirstName");
            
            migrationBuilder.DropColumn("AspNetUsers", "LastName");
            
            migrationBuilder.AlterColumn("Address", "AddressType", c => c.Int(nullable: false));
        }
    }
}