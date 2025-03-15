using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class S7ConfigResposity:BaseRespository<S7Config>,IS7ConfigResposity
    {
        private readonly MyDbContext _dbContext;

        public S7ConfigResposity(MyDbContext dbContext)
        {
            base._ctx = dbContext;
            _dbContext = dbContext;
        }
        /// <summary>
        /// 根据配置名检查是否存在该配置
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        public async Task<bool> FindEntityByNameAsync(string proxyName)
        {
            return await _dbContext.s7Configs.AnyAsync(x => x.ProxyName == proxyName);
        }
    }
}
