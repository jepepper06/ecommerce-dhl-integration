using Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Model;
using Services;
using Services.Admin;
using Services.ApiAdapter;
using Services.Cart;
using Services.Register;
using Services.Repositories;
using Services.Shopping;
using Services.Validators;

namespace Api.Startup;

public static class Startup
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    { 
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<DhlOptions>(configuration.GetSection(DhlOptions.DHL));
        var dbConnection = configuration.GetConnectionString("PostgreSQL");
        services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap.Add("uint", typeof(IntRouteConstraint));
        });
        services.AddDbContext<EcommerceDbContext>(options => {
            options.UseNpgsql(dbConnection); 
        });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddHttpClient("DhlClient");
        services.AddScoped<IValidator<CustomerRegistrationDTO>, CustomerValidator>();
        services.AddScoped<IValidator<ProductCreationDTO>, ProductValidator>();
        services.AddScoped<IValidator<SupplierCreationDTO>, SupplierValidator>();
        services.AddScoped<IValidator<ProductUpdateDTO>, ProductUpdateValidator>();

        services.AddScoped<IRepository<Customer>,CustomerRepository>();
        services.AddScoped<IRepository<Product>,ProductRepository>();        
        services.AddScoped<IRepository<Supplier>,SupplierRepository>();        
        services.AddScoped<IRepository<Item>,ItemRepository>();        
        services.AddScoped<IRepository<Order>,OrderRepository>();


        services.AddScoped<IDeserializationHelper<DhlServiceResponse>,DhlDeserializationHelper>();
        services.AddScoped<IDhlServiceAdapter,DhlServiceAdapter>();
        services.AddScoped<ITrackingService,TrackingService>();
        services.AddScoped<IAdminService,AdminService>();
        services.AddScoped<ICartService,CartService>();
        services.AddScoped<IRegistrationService,RegistrationService>();
        services.AddScoped<IShoppingService,ShoppingService>();


        return services;
    }
}