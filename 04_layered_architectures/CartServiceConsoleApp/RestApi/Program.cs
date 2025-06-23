using CatalogService.DataAccess.RabbitMQ;
using CatalogService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RestApi.Messaging;
using RestApi.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                              new HeaderApiVersionReader("x-api-version"),
                                                              new MediaTypeApiVersionReader("x-api-version"));
        });

        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        var cartConnection = builder.Configuration.GetConnectionString("DatabasePath");

        builder.Services.AddInfrastructure(connectionString, cartConnection);
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        //RabbitMQ
        builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
        builder.Services.AddHostedService<CartMessageListener>();

        builder.Services.Configure<HostOptions>(options =>
        {
            options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
        });
        if (args.Any(a => a.Contains("ef")))
        {
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(80); // HTTP only for migrations
            });
        }
        else
        {
            builder.WebHost.UseKestrel(options =>
            {
                options.ListenAnyIP(80);
                options.ListenAnyIP(443, listenOptions => listenOptions.UseHttps());
            });
        }

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSwaggerGen(c =>
        {
            //iclude auto-generated doc
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();

        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Add root endpoint
        app.MapGet("/", async context => await context.Response.WriteAsync("Welcome to CartService API!"));

        // Add health check endpoint
        app.MapGet("/api/health", async context => await context.Response.WriteAsync("Healthy"));

        app.Run();
    }
}