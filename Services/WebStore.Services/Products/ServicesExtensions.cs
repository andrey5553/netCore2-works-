using Microsoft.Extensions.DependencyInjection;
using WebStore.Interfaces.Services;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InSQL;

namespace WebStore.Services.Products
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddWebStoreServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeesService, SqlEmployeesService>();
            services.AddScoped<IProductService, SqlProductData>();
            services.AddScoped<IOrdersService, SqlOrderService>();
            services.AddScoped<ICartService, CookieCartService>();
            return services;
        }
    }
}
