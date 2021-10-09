using Api.Data.Entities;
using Api.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class DbContextFiller
    {
        public static void HasData(ModelBuilder modelBuilder)
        {
            var roles = new Role[]
            {
                new Role { Id = 1, Name = Roles.Creator, NormalizedName = Roles.Creator.ToUpper() },
                new Role { Id = 1, Name = Roles.Executor, NormalizedName = Roles.Executor.ToUpper() },
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}
