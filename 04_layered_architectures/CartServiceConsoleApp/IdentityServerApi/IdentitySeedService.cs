using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServerApi
{
    public class IdentitySeedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IdentitySeedService> _logger;

        public IdentitySeedService(IServiceProvider serviceProvider, ILogger<IdentitySeedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed roles
            string[] roles = { "Manager", "StoreCustomer" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("Failed to create role {Role}: {Errors}",
                            role, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                        throw new InvalidOperationException($"Failed to create role {role}");
                    }
                    _logger.LogInformation("Created role {Role}", role);
                }
            }

            // Seed manager1
            var manager1 = new IdentityUser { UserName = "manager1", Email = "manager1@example.com" };
            if (await userManager.FindByNameAsync("manager1") == null)
            {
                var createResult = await userManager.CreateAsync(manager1, "Manager@123"); // Stronger password
                if (!createResult.Succeeded)
                {
                    _logger.LogError("Failed to create user {User}: {Errors}",
                        manager1.UserName, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Failed to create user {manager1.UserName}");
                }
                _logger.LogInformation("Created user {User}", manager1.UserName);

                var roleResult = await userManager.AddToRoleAsync(manager1, "Manager");
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to add role Manager to user {User}: {Errors}",
                        manager1.UserName, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Failed to add role Manager to user {manager1.UserName}");
                }
                _logger.LogInformation("Added role Manager to user {User}", manager1.UserName);
            }

            // Seed customer1
            var customer1 = new IdentityUser { UserName = "customer1", Email = "customer1@example.com" };
            if (await userManager.FindByNameAsync("customer1") == null)
            {
                var createResult = await userManager.CreateAsync(customer1, "Customer@123");
                if (!createResult.Succeeded)
                {
                    _logger.LogError("Failed to create user {User}: {Errors}",
                        customer1.UserName, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Failed to create user {customer1.UserName}");
                }
                _logger.LogInformation("Created user {User}", customer1.UserName);

                var roleResult = await userManager.AddToRoleAsync(customer1, "StoreCustomer");
                if (!roleResult.Succeeded)
                {
                    _logger.LogError("Failed to add role StoreCustomer to user {User}: {Errors}",
                        customer1.UserName, string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    throw new InvalidOperationException($"Failed to add role StoreCustomer to user {customer1.UserName}");
                }
                _logger.LogInformation("Added role StoreCustomer to user {User}", customer1.UserName);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}