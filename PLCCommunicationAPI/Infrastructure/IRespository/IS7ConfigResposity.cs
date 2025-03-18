using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IRespository
{
    public interface IS7ConfigResposity : IBaseRespository<S7Config>
    {
        public Task<bool> FindEntityByNameAsync(string proxyName);


        public Task<bool> DeletedAsync(S7Config entity);
        public Task<bool> Getstate( int id);

        public Task<bool> Enable(int  id);
        public Task<bool> Disable(int id);

        public Task<bool> UpdateAsync(S7Config isExist, S7Config s7Config);
    }
}
