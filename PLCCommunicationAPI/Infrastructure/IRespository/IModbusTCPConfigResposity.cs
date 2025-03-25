using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.MiniExcelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IRespository
{
    public interface IModbusTCPConfigResposity:IBaseRespository<ModbusTCPConfig>
    {
        public  Task<bool> CreateAsync(ModbusTCPConfig modbusTCP);

        public Task<bool> DeletedByIdAsync(List<int> ids);
        public Task<bool[]> ReadCoilsAsync(int id);
        public Task<bool[]> ReadDiscreteInputsAsync(int id);
        public Task<ushort[]> ReadHoldingRegistersAsync(int id);
        public Task<bool> UpdateAsync(ModbusTCPConfig isExist, ModbusTCPConfig modbusTCPConfig);
        public Task<bool> WriteSingCoilsAsync(int id, bool value);

        Task BulkUpsertAsync(IEnumerable<ModbusTCPExcel> configs);

        Task<bool> ExistsByProxyName(string proxyName);
    }
}
