using BusinesLayer.Services.MasterData;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IMasterDataService, MasterDataService>();
            //services.AddScoped<IMenuService, MenuService>();
            //services.AddScoped<IMasterDataService, MasterDataService>();
            //services.AddScoped<IRepeatTaskService, RepeatTaskService>();
            return services;
        }
    }
}
