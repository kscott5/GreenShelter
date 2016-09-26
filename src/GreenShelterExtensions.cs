using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    public static class GreenShelterExtensions
    {
        public static async void EnsureDbContextCreatedAndSeeded(this IServiceProvider serviceProvider) {
            await Task.Run(()=>{
                var dbContext = serviceProvider.GetService<GreenShelterDbContext>();

                if(dbContext.Database.EnsureCreated()) {
                    // Database was just created
                    // TODO: How do I get the Migrations.Data.* classes to seed the database 
                }
            });
        } // end EnsureDbContextCreatedAndSeeded
    }
}
