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
    }
}
