using FastFood.Application.Interfaces;
using FastFood.Application.UseCases;
using FastFood.DataSource;
using FastFood.Domain.Interfaces;
using FastFood.Infra.Data.Context;
using FastFood.Infra.Data.Repository;
using FastFood.Infra.ExternalServices;
using FastFood.Infra.ExternalServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Infra.IoC;

public static class DependencyInjectionResolver
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        //DbContext
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<FastFoodDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IDataSource, FastFoodDataSource>();

        //UseCases
        services.AddScoped<IUserUseCases, UserUseCases>();
        services.AddScoped<IProductUseCases, ProductUseCases>();
        //services.AddScoped<ICategoryUseCases, CategoryUseCases>();
        services.AddScoped<IOrderUseCases, OrderUseCases>();
        services.AddScoped<ICartUseCases, CartUseCases>();
        services.AddScoped<ICartItemUseCases, CartItemUseCases>();
        //services.AddScoped<IPermissionUseCases, PermissionUseCases>();
        services.AddScoped<IUserUseCases, UserUseCases>();
        services.AddScoped<IPaymentUseCases, PaymentUseCases>();

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        //services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICartItemRepository, CartItemRepository>();
        //services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        
        //ExternalServices
        services.AddScoped<IMercadoPagoService, MercadoPagoService>();

        return services;
    }
}