using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTasks.ModbusTcp
{
    public class ModbusTCP:IModbusTCP
    {
        private readonly IServiceProvider _services;

        //private readonly S7ProxyToTask _s7ProxyToTask;

        //private readonly S7ReadTaskResposity _s7ReadTaskResposity;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly ILogger<ModbusTCP> _logger;

        private readonly MyDbContext _ctx;
        public ModbusTCP(MyDbContext ctx, ILogger<ModbusTCP> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }


        /// <summary>
        /// 初始化后台ModbusTCP配置
        /// </summary>
        /// <returns></returns>
        public async Task<bool> InitializeAsync()
        {
            var Modbus = await _ctx.modbusTCPConfigs.ToListAsync();
            foreach(var item in Modbus)
            {
                var isExist = await _ctx.modbusTCPBackgroundServices.FirstOrDefaultAsync(x => x.ProxyName == item.ProxyName);
                if(isExist == null)
                {
                    var modbusTCPBackgroundService = new ModbusTCPBackgroundService()
                    {
                        ProxyName = item.ProxyName,
                        IP = item.IP,
                        Port = item.Port,
                        SlaveID = item.SlaveID,
                        FunctionCode = item.FunctionCode,
                        StartAddress = item.StartAddress,
                        Num = item.Num,
                        IsOpen = item.IsOpen,
                        IsDeleted = item.IsDeleted,
                        Createtime = item.Createtime,
                        LastModified = item.LastModified,
                    };
                   _ctx.modbusTCPBackgroundServices.Add(modbusTCPBackgroundService);
                   return  await _ctx.SaveChangesAsync()>0;
                }
                
            }
            return false;
        }
    }
}
