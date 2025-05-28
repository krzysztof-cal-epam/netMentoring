using CatalogService.Application.BackgroundServices;
using CatalogService.Application.Interfaces;
using CatalogService.Application.Services;
using CatalogService.DataAccess.Data;
using CatalogService.DataAccess.Repositories;
using CatalogService.Domain.Databases;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionString,
            string cartConnectionString)
        {
            services.AddDbContext<CatalogDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
            services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<CatalogService.Application.Interfaces.ICartService, CatalogService.Application.Services.CartService>();

            services.AddScoped<DataAccess.Interfaces.ICartService, DataAccess.Services.CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddSingleton<ICartDatabase<Cart>>(provider =>
            {
                return new LiteDbCartDatabase(cartConnectionString);
            });

            services.AddHostedService<OutboxProcessor>();

            return services;
        }
    }
}
