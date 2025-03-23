using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Entities;
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

        public Task<bool> UpdateAsync(ModbusTCPConfig isExist, ModbusTCPConfig modbusTCPConfig);
    }
}
