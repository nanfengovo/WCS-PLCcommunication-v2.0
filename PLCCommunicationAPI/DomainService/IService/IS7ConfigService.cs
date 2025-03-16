using PLCCommunication_DomainService.IBaseService;
using PLCCommunication_Infrastructure.Migrations;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.IService
{
    public interface IS7ConfigService : IBaseService<S7Config>
    {
       public  Task<bool> FindEntityByNameAsync(string proxyName);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> DeletedAsync(S7Config entity);
    }
}
