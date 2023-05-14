using EcommerceShop.Business.Interfaces;
using EcommerceShop.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Business
{
    public static class ServiceRegister
    {
        public static void AddBusinessLayer(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IPublicProductService, PublicProductService>();
            services.AddTransient<IManageProductService, ManageProductService>();
            services.AddTransient<IStorageService, FileStorageService>();

        }
    }
}
