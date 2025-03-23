using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class ModbusTCPConfigResposity : BaseRespository<ModbusTCPConfig>, IModbusTCPConfigResposity
    {
        private readonly MyDbContext _dbContext;

        private readonly ILogger<ModbusTCPConfig> _logger;

        public ModbusTCPConfigResposity(MyDbContext dbContext, ILogger<ModbusTCPConfig> logger)
        {
            base._ctx = dbContext;
            _dbContext = dbContext;
            _logger = logger;
        }


        /// <summary>
        /// 创建新的Modbus配置
        /// </summary>
        /// <param name="modbusTCP"></param>
        /// <returns></returns>
        public async Task<bool> CreateAsync(ModbusTCPConfig modbusTCP)
        {
            var isExist = await _dbContext.modbusTCPConfigs.FirstOrDefaultAsync(x => x.ProxyName == modbusTCP.ProxyName);
            if (isExist == null)
            {
                _logger.LogInformation($"创建新的Modbus配置——配置名为{modbusTCP.ProxyName}的配置创建成功！");
                _dbContext.modbusTCPConfigs.Add(modbusTCP);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            else
            {
                _logger.LogWarning($"创建新的Modbus配置——配置名为{modbusTCP.ProxyName}的配置已经存在！");
                return false;
            }
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// <summary>
        /// 根据id批量删除modbusTCP配置
        /// </summary>
        /// <param name="ids">要删除的配置ID列表</param>
        /// <returns>是否全部删除成功</returns>
        public async Task<bool> DeletedByIdAsync(List<int> ids)
        {
            // 去除重复的ID
            var distinctIds = ids.Distinct().ToList();

            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // 批量查询所有需要删除的配置
                var modbusList = await _dbContext.modbusTCPConfigs
                    .Where(x => distinctIds.Contains(x.Id))
                    .ToListAsync();

                // 检查是否存在无效ID
                var foundIds = modbusList.Select(x => x.Id).ToList();
                var missingIds = distinctIds.Except(foundIds).ToList();
                if (missingIds.Count > 0)
                {
                    _logger.LogError($"删除失败：以下ID不存在 - {string.Join(",", missingIds)}");
                    await transaction.RollbackAsync();
                    return false;
                }

                // 预检查所有配置状态
                foreach (var modbus in modbusList)
                {
                    if (modbus.IsDeleted)
                    {
                        _logger.LogError($"删除失败：配置 {modbus.ProxyName}(ID:{modbus.Id}) 已删除");
                        await transaction.RollbackAsync();
                        return false;
                    }

                    if (modbus.IsOpen)
                    {
                        _logger.LogError($"删除失败：配置 {modbus.ProxyName}(ID:{modbus.Id}) 正在运行");
                        await transaction.RollbackAsync();
                        return false;
                    }
                }

                // 批量更新删除状态
                modbusList.ForEach(x => x.IsDeleted = true);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                // 记录成功日志
                modbusList.ForEach(x =>
                    _logger.LogInformation($"成功删除配置：{x.ProxyName}(ID:{x.Id})"));

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "批量删除操作异常");
                return false;
            }
        }

        public override async Task<List<ModbusTCPConfig>> FindAllAsync()
        {
            return await _dbContext.modbusTCPConfigs.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public override Task<List<ModbusTCPConfig>> FindAllAsync(Expression<Func<ModbusTCPConfig, bool>> del)
        {
            return base.FindAllAsync(del);
        }

        public override Task<ModbusTCPConfig> FindEntityByAsync(Expression<Func<ModbusTCPConfig, bool>> del)
        {
            return base.FindEntityByAsync(del);
        }

        public override Task<ModbusTCPConfig> FindEntityByIdAsync(Guid id)
        {
            return base.FindEntityByIdAsync(id);
        }
    }
}
