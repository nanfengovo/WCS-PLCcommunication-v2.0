using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.Plug_ins
{
    public static class IOCExtend
    {
        /// <summary>
        /// 注入自定义接口
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            //注入仓储层
            services.AddScoped<IModbusTCPConfigResposity, ModbusTCPConfigResposity>();
            services.AddScoped<IUserRespository, UserRespository>();
            services.AddScoped<IS7ConfigResposity, S7ConfigResposity>();
            services.AddScoped<IS7ReadWriteResposity, S7ReadWriteResposity>();


            //注入服务层
            services.AddScoped<IModbusTCPConfigService, ModbusTCPConfigService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IS7ConfigService, S7ConfigService>();
            services.AddScoped<IS7ReadWriteService, S7ReadWriteService>();

            return services;
        }


      
    }
}
