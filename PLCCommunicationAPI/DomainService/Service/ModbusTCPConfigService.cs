using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;
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

        public async Task<bool> DeletedById(List<int> ids)
        {
            return await _modbusTCPConfigResposity.DeletedByIdAsync(ids);
        }

        public async Task<bool> UpdateAsync(ModbusTCPConfig isExist, ModbusTCPConfig modbusTCPConfig)
        {
            return await _modbusTCPConfigResposity.UpdateAsync(isExist,modbusTCPConfig);
        }

        public Task<bool[]> ReadCoilsAsync(int id)
        {
            return _modbusTCPConfigResposity.ReadCoilsAsync(id);
        }

        public async Task<bool[]> ReadDiscreteInputsAsync(int id)
        {
            return await _modbusTCPConfigResposity.ReadDiscreteInputsAsync(id);
        }

        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ushort[]> ReadHoldingRegistersAsync(int id)
        {
            return await _modbusTCPConfigResposity.ReadHoldingRegistersAsync(id);
        }

        /// <summary>
        /// 写入线圈
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> WriteSingCoilsAsync(int id, bool value)
        {
            return await _modbusTCPConfigResposity.WriteSingCoilsAsync(id, value);
        }
    }
}
