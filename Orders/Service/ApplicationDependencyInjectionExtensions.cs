using Ninject;
using Ninject.Modules;
using Orders.Service.OrderItems;
using Orders.Service.Orders;
using Orders.Service.Providers;

namespace Orders.Service
{
    public static class ApplicationDependencyInjectionExtensions
    {
        public static IServiceCollection AddOrderService(this IServiceCollection services)
        {
            return services
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IOrderItemService, OrderItemService>()
                .AddScoped<IProviderService, ProviderService>();
        }
    }
}
