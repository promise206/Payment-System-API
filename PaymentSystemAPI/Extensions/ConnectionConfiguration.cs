using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaymentSystem.Domain.Entities;
using PaymentSystem.Infrastructure;

namespace PaymentSystemAPI.Extensions
{
    public static class ConnectionConfiguration
    {
        public static void AddDbContextAndConfigurations(this IServiceCollection services, IWebHostEnvironment env, IConfiguration config)
        {
            services.AddDbContextPool<CustomerDbContext>(options =>
            {
                string connStr =  config.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connStr);
            });

            var builder = services.AddIdentity<Customer, IdentityRole>(x =>
            {
                x.Password.RequiredLength = 8;
                x.Password.RequireDigit = false;
                x.Password.RequireUppercase = true;
                x.Password.RequireLowercase = true;
                x.User.RequireUniqueEmail = true;
                x.SignIn.RequireConfirmedEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            _ = builder.AddEntityFrameworkStores<CustomerDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}
