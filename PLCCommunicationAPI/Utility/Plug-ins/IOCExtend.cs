using Microsoft.Extensions.DependencyInjection;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.Plug_ins
{
    public static class IOCExtend
    {
        public static IServiceCollection AddCustomIOC(this IServiceCollection services)
        {
            //注入仓储层
            services.AddScoped<IModbusTCPConfigResposity, ModbusTCPConfigResposity>();



            //注入服务层
            services.AddScoped<IModbusTCPConfigService, ModbusTCPConfigService>();

            return services;
        }
    }
}
