using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.Identity;
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



        public override async Task<List<S7Config>> FindAllAsync()
        {
            return await _dbContext.s7Configs.Where(x => x.IsDeleted == false).ToListAsync();
        }


        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> DeletedAsync(S7Config entity)
        {
            // 查找具有相同Id的实体
            var existingEntity = await _dbContext.s7Configs.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (existingEntity == null)
            {
                // 如果实体不存在，则返回false
                return false;
            }

            // 将IsDeleted属性设置为false
            existingEntity.IsDeleted = true;

            // 保存更改到数据库
            var result = await _dbContext.SaveChangesAsync() > 0;

            return result; // 如果成功保存更改，则返回true，否则返回false
        }
    }
}
