using Microsoft.Extensions.DependencyInjection;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Products
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddWebStoreServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, SqlProductService>();
            services.AddScoped<IOrdersService, SqlOrdersService>();
            services.AddScoped<ICartService, CookieCartService>();
            return services;
        }
    }
}
