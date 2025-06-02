using CatalogService.Application.Interfaces;
using CatalogService.Application.Services;
using CatalogService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Net;

AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false); 
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); 
var connectionString2 = builder.Configuration.GetConnectionString("DatabasePath"); 
builder.Services.AddInfrastructure(connectionString, connectionString2);

builder.Services.AddScoped<ICategoryService, CategoryService>(); 
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(); 
builder.Services.AddLogging(logging =>
{ 
    logging.AddConsole(); 
    logging.AddDebug(); 
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = "https://localhost:7191";
    options.Audience = "CatalogApi";
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new() 
    { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
        RoleClaimType = "role", 
        NameClaimType = "sub" };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("Read", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                (c.Type == "role" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role") &&
                (c.Value.Equals("Manager", StringComparison.OrdinalIgnoreCase) ||
                 c.Value.Equals("StoreCustomer", StringComparison.OrdinalIgnoreCase)))))
    .AddPolicy("Manage", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
                (c.Type == "role" || c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role") &&
                c.Value.Equals("Manager", StringComparison.OrdinalIgnoreCase))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger(); 
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();