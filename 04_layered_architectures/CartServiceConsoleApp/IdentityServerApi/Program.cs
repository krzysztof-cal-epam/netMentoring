using IdentityServerApi;
using Serilog;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServerApi.Data;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));
    
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });

    builder.ConfigureServices();

    var app = builder.Build();

    app.UseCors("AllowAll");

    app.ConfigurePipeline();

    if (args.Contains("/seed"))
    {
        Log.Information("Seeding database...");
        using (var scope = app.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            await EnsureRole(roleManager, "Manager");
            await EnsureRole(roleManager, "StoreCustomer");

            var user = await userManager.FindByNameAsync("manager1");
            if (user == null)
            {
                user = new IdentityUser { UserName = "manager1", Email = "manager1@example.com" };
                var result = await userManager.CreateAsync(user, "Manager@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Manager");
                }
                else
                {
                    Log.Error("Failed to create user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
        Log.Information("Done seeding database. Exiting.");
        return;
    }

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

async Task EnsureRole(RoleManager<IdentityRole> roleManager, string roleName)
{
    if (!await roleManager.RoleExistsAsync(roleName))
    {
        var result = await roleManager.CreateAsync(new IdentityRole(roleName));
        if (!result.Succeeded)
        {
            Log.Error("Failed to create role {Role}: {Errors}", roleName, string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}