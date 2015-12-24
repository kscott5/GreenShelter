using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using PCSC.GreenShelter.Models;

namespace PCSC.GreenShelter
{
    public static class GreenShelterExtensions
    {
        public static async void EnsureDbContextCreatedAndSeeded(this IServiceProvider serviceProvider) {
            var logger = serviceProvider.GetService<LoggerFactory>().CreateLogger("GreenShelterExtensions");
            
            var dbContext = serviceProvider.GetService<GreenShelterDbContext>();
            if(!dbContext.Database.EnsureCreated()) {
                throw new ApplicationException("Failed to create the database");
            } else {
                dbContext.EnsureCreatedAndSeeded(serviceProvider);
            }
        } // end EnsureDbContextCreatedAndSeeded
    }
}
