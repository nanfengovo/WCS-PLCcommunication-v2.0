using Microsoft.AspNetCore.Http;
using PLCCommunication_DomainService.IBaseService;
using PLCCommunication_Infrastructure.Migrations;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.MiniExcelHelper;
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

        /// <summary>
        /// 查询当前状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> Getstate(int id);
        public Task<bool> Enable(int id);
        public Task<bool> Disable(int id);

        public Task<bool> UpdateAsync(S7Config isExist,S7Config s7Config);

        Task<ImportResult> ImportConfigsAsync(IFormFile file);
        Task<Stream> ExportConfigsAsync();
    }
}
