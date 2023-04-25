using EcommerceShop.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Data
{
    public static class ServiceRegister
    {
        public static void AddDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EcommerceShopDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"), b =>
                    b.MigrationsAssembly(typeof(EcommerceShopDbContext).Assembly.FullName)
            ));
        }
    }
}
