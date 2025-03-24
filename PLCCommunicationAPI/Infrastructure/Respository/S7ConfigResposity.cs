using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.Identity;
using PLCCommunication_Model.MiniExcelModel;
using Polly;
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
            existingEntity.LastModified = DateTime.Now;         

            // 保存更改到数据库
            var result = await _dbContext.SaveChangesAsync() > 0;

            return result; // 如果成功保存更改，则返回true，否则返回false
        }

        public async Task<bool> Getstate( int id)
        {
            var s7config = await _dbContext.s7Configs.FirstOrDefaultAsync(x => x.Id == id);

                return s7config.IsOpen;

            
        }


        public async Task<bool> Enable(int id)
        {
            var s7Config= await _dbContext.s7Configs.FirstOrDefaultAsync(x => x.Id == id);
            s7Config.IsOpen = true;
            s7Config.LastModified = DateTime.Now;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Disable(int id)
        {
            var s7Config = await _dbContext.s7Configs.FirstOrDefaultAsync(x => x.Id == id);
            s7Config.IsOpen = false;
            s7Config.LastModified = DateTime.Now;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(S7Config isExist, S7Config s7Config)
        {
            isExist.ProxyName = s7Config.ProxyName;
            isExist.IP = s7Config.IP;
            isExist.Port = s7Config.Port;
            isExist.DBID = s7Config.DBID;
            isExist.Address = s7Config.Address;
            isExist.Type = s7Config.Type;
            isExist.Length = s7Config.Length;
            isExist.bit = s7Config.bit;
            isExist.Remark = s7Config.Remark;
            isExist.IsOpen = s7Config.IsOpen;
            isExist.LastModified = DateTime.Now;

            return await _dbContext.SaveChangesAsync() > 0;
        }


        public async Task BulkUpsertAsync(IEnumerable<S7Excel> configs)
        {
            foreach (var config in configs)
            {
                var existing = await _dbContext.s7Configs
                    .FirstOrDefaultAsync(c => c.ProxyName == config.ProxyName);

                if (existing != null)
                {
                    // 更新逻辑
                    existing.IP = config.IP;
                    existing.Port = config.Port;
                    existing.DBID = config.DBID;
                    existing.Address = config.Address;
                    existing.Type = config.Type;
                    existing.Length = config.Length;
                    existing.bit = config.bit;
                    existing.Remark = config.Remark;
                    existing.LastModified = DateTime.Now;

                }
                else
                {
                    // 新增逻辑
                    var newConfig = new S7Config
                    {
                        ProxyName = config.ProxyName,
                        IP = config.IP,
                        Port = config.Port,
                        DBID = config.DBID,
                        Address = config.Address,
                        Type = config.Type,
                        Length = config.Length,
                        bit = config.bit,
                        Remark = config.Remark,
                        IsOpen = true,
                        IsDeleted =false,
                        Createtime = DateTime.Now,
                        LastModified = DateTime.Now

                    };
                    await _dbContext.s7Configs.AddAsync(newConfig);
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<S7Config>> GetAllAsync()
        {
            return await _dbContext.s7Configs
                .AsNoTracking()
                .OrderBy(c => c.ProxyName)
                .ToListAsync();
        }

        public async Task<bool> ExistsByProxyName(string proxyName)
        {
            return await _dbContext.s7Configs
                .AnyAsync(c => c.ProxyName == proxyName);
        }

       
    }
}
