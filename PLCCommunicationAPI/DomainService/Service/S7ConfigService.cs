using Microsoft.Extensions.Logging;
using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Migrations;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.Service
{
    public class S7ConfigService:BaseService<S7Config>,IS7ConfigService
    {
        private readonly IS7ConfigResposity _s7ConfigResposity;

        private readonly ILogger<S7ConfigService> _logger;

        public S7ConfigService(IS7ConfigResposity s7ConfigResposity, ILogger<S7ConfigService> logger)
        {
            _s7ConfigResposity = s7ConfigResposity;
            _logger = logger;
            base._respository = s7ConfigResposity;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> DeletedAsync(S7Config entity)
        {
            return await _s7ConfigResposity.DeletedAsync(entity);
        }

        /// <summary>
        /// 根据配置名判断现有的配置是否存在
        /// </summary>
        /// <param name="proxyName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> FindEntityByNameAsync(string proxyName)
        {
            return await _s7ConfigResposity.FindEntityByNameAsync(proxyName);
        }


    }
}
