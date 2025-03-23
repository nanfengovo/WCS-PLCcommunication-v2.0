using PLCCommunication_DomainService.IBaseService;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.IService
{
    public interface IModbusTCPConfigService:IBaseService<ModbusTCPConfig>  
    {
        public Task<bool> DeletedById(List<int> ids);
        public Task<bool[]> ReadCoilsAsync(int id);
        public Task<bool[]> ReadDiscreteInputsAsync(int id);
        public Task<ushort[]> ReadHoldingRegistersAsync(int id);
        public Task<bool> UpdateAsync(ModbusTCPConfig isExist, ModbusTCPConfig modbusTCPConfig);
        public Task<bool> WriteSingCoilsAsync(int id, bool value);
    }
}
