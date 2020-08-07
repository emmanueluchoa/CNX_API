using CNX_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CNX_Repository.Context
{
    public class SeedDataBase
    {
        public static async Task InitializeDataBase(IServiceProvider serviceProvider)
        {
            var cnxContext = serviceProvider.GetRequiredService<CnxContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            cnxContext.Database.EnsureCreated();

            if (!cnxContext.Users.Any())
            {
                User user = new User
                {
                    UserName = "EmmanuelCavalcanti",
                    Email = "uchoa.emmanuel@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Locale = "Igarassu,BR"
                };

                var teste = await userManager.CreateAsync(user, "cnx1234567");
            }
        }
    }
}
