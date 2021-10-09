using Api.Data.Entities;
using Api.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data
{
    public class DbContextFiller : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DbContextFiller(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();

            //await Task.Delay(TimeSpan.FromSeconds(30));
            var roles = new Role[]
            {
                new Role { Name = Roles.Creator, NormalizedName = Roles.Creator.ToUpper() },
                new Role { Name = Roles.Executor, NormalizedName = Roles.Executor.ToUpper() },
            };

            using var context = scope.ServiceProvider.GetService<ApplicationContext>();

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            await context.SaveChangesAsync();

            //Task.Run(Fill);
        }

        private async Task Fill()
        {
            await Task.Delay(TimeSpan.FromSeconds(30));
            var roles = new Role[]
            {
                new Role { Name = Roles.Creator, NormalizedName = Roles.Creator.ToUpper() },
                new Role { Name = Roles.Executor, NormalizedName = Roles.Executor.ToUpper() },
            };

            using var context = _serviceProvider.GetService<ApplicationContext>();

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }

            await context.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
