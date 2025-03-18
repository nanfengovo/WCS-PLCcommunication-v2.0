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

        /// <summary>
        /// 查询当前状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Getstate(int id)
        {
            return await _s7ConfigResposity.Getstate( id);
        }


        public async Task<bool> Enable(int id)
        {
            return await _s7ConfigResposity.Enable(id);
        }

        public async Task<bool> Disable(int id)
        {
            return await _s7ConfigResposity.Disable(id);
        }

        public async Task<bool> UpdateAsync(S7Config isExist, S7Config s7Config)
        {
            return await _s7ConfigResposity.UpdateAsync(isExist,s7Config);
        }
    }
}
