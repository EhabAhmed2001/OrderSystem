using OrderSystem.Core;
using OrderSystem.Core.Repository;
using OrderSystem.Core.Service;
using OrderSystem.PL.Helper;
using OrderSystem.Repository;
using OrderSystem.Service;

namespace OrderSystem.PL.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfwork));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            
            services.AddAutoMapper(typeof(Mapping));

            return services;
        }
    }
}
