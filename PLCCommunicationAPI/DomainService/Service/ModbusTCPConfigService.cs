using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.Service
{
    public class ModbusTCPConfigService : BaseService<ModbusTCPConfig>, IModbusTCPConfigService
    {
        private readonly IModbusTCPConfigResposity _modbusTCPConfigResposity;

        public ModbusTCPConfigService(IModbusTCPConfigResposity modbusTCPConfigResposity)
        {
            base._respository = modbusTCPConfigResposity;
            _modbusTCPConfigResposity = modbusTCPConfigResposity;
        }


        public async Task<List<ModbusTCPConfig>> GetAll()
        {
            return await base._respository.FindAllAsync();
        }

        /// <summary>
        /// 创建ModbusTCP配置
        /// </summary>
        /// <param name="modbusTCPConfig"></param>
        /// <returns></returns>
        public async Task<bool> Create(ModbusTCPConfig modbusTCPConfig)
        {
            return await base._respository.CreateAsync(modbusTCPConfig);
        }
    }
}
