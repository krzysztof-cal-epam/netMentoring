using CartServiceConsoleApp.BLL.Interfaces;
using CartServiceConsoleApp.BLL.Services;
using CartServiceConsoleApp.DAL.Databases;
using CartServiceConsoleApp.DAL.Interfaces;
using CartServiceConsoleApp.DAL.Repositories;
using CartServiceConsoleApp.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartDatabase<Cart>>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connection = configuration["LiteDb:DatabasePath"];

    return new LiteDbCartDatabase(connection);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
