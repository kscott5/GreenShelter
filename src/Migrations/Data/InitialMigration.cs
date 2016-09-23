using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore.Migrations;

// TODO: How to configure namespace for entityframe commands
namespace src.Migrations
{
    public partial class InitialMigration
    {        
        protected void UpData(MigrationBuilder migrationBuilder)
        {
            // NOTE: AutoIncrement columns can be explicit set for SQLite. But SQL Server requires you to call 'set identity_insert'
            
            var roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
            
            // Adminstrator role and claim  
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoles (RoleId, ConcurrencyStamp, Description, Name, NormalizedName) VALUES (1, '{0}', 'Green Shelter application user with administrator access', 'Administrator', 'Administrator');", Guid.NewGuid().ToString()));
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoleClaims (Id, ClaimType, ClaimValue, RoleId) VALUES (1, '{0}', 'Administrator', 1);", roleClaim));
            
            // Organization role and claim  
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoles (RoleId, ConcurrencyStamp, Description, Name, NormalizedName) VALUES (2, '{0}', 'Green Shelter application user with organization access', 'Organization', 'Organization');", Guid.NewGuid().ToString()));
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoleClaims (Id, ClaimType, ClaimValue, RoleId) VALUES (2, '{0}', 'Organization', 2);", roleClaim));
            
            // Volunteer role and claim  
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoles (RoleId, ConcurrencyStamp, Description, Name, NormalizedName) VALUES (3, '{0}', 'Green Shelter application user with volunteer access', 'Volunteer', 'Volunteer');", Guid.NewGuid().ToString()));
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoleClaims (Id, ClaimType, ClaimValue, RoleId) VALUES (3, '{0}', 'Volunteer', 3);", roleClaim));
            
            // Client role and claim  
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoles (RoleId, ConcurrencyStamp, Description, Name, NormalizedName) VALUES (4, '{0}', 'Green Shelter application user with client access', 'Client', 'Client');", Guid.NewGuid().ToString()));
            migrationBuilder.Sql(string.Format("INSERT INTO AspNetRoleClaims (Id, ClaimType, ClaimValue, RoleId) VALUES (4, '{0}', 'Client', 4);", roleClaim));
        }

        protected void DownData(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(string.Format("DELETE * FROM AspNetRoleClaims;"));
            migrationBuilder.Sql(string.Format("DELETE * FROM AspNetRoles;"));            
        }
    }
}
