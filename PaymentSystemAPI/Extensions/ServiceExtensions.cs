using Microsoft.Extensions.Options;
using PaymentSystem.Core.Interfaces;
using PaymentSystem.Core.Services;
using PaymentSystem.Core.Utility;
using PaymentSystem.Infrastructure.Interfaces;
using PaymentSystem.Infrastructure.Repository;
using System.Reflection;

namespace PaymentSystemAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(PaymentSystemProfile));

            // Register dependencies here
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
        }
    }
}
