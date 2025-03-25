using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NModbus;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.MiniExcelModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
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

        /// <summary>
        /// 读取线圈
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool[]> ReadCoilsAsync(int id)
        {
            //1、根据id先判断是否存在
            var modbustcp = _dbContext.modbusTCPConfigs.FirstOrDefault(x => x.Id == id);
            if (modbustcp == null)
            {
                _logger.LogError("读取线圈——未找到对应的ModbusTCP配置！");
                return null;
            }
            //2、根据配置信息拿到对应的ip和端口
            var ip = modbustcp.IP;
            var port = modbustcp.Port;
            //3、根据ip和端口建立对应的plc连接
            try
            {
                using (var client = new TcpClient(ip,port))
                {
                    var factory = new ModbusFactory();
                    var master = factory.CreateMaster(client);

                    ushort startAddress = modbustcp.StartAddress;
                    ushort numRegisters = modbustcp.Num; // 读取的寄存器数量

                    var result = await master.ReadCoilsAsync(modbustcp.SlaveID, startAddress, numRegisters);
                    return result;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "读取线圈——读取线圈时发生错误！");
                return null;
            }
        }

        /// <summary>
        /// 读取离散寄存器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool[]> ReadDiscreteInputsAsync(int id)
        {
            //1、根据id先判断是否存在
            var modbustcp = _dbContext.modbusTCPConfigs.FirstOrDefault(x => x.Id == id);
            if (modbustcp == null)
            {
                _logger.LogError("读取离散寄存器——未找到对应的ModbusTCP配置！");
                return null;
            }
            //2、根据配置信息拿到对应的ip和端口
            var ip = modbustcp.IP;
            var port = modbustcp.Port;
            //3、根据ip和端口建立对应的plc连接
            try
            {
                using (var client = new TcpClient(ip, port))
                {
                    var factory = new ModbusFactory();
                    var master = factory.CreateMaster(client);

                    ushort startAddress = modbustcp.StartAddress;
                    ushort numRegisters = modbustcp.Num; // 读取的寄存器数量

                    var result = await master.ReadInputsAsync(modbustcp.SlaveID, startAddress, numRegisters);
                    return result;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "读取离散寄存器——读取线圈时发生错误！");
                return null;
            }
        }

        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ushort[]> ReadHoldingRegistersAsync(int id)
        {
            //1、根据id先判断是否存在
            var modbustcp = _dbContext.modbusTCPConfigs.FirstOrDefault(x => x.Id == id);
            if (modbustcp == null)
            {
                _logger.LogError("读取保持寄存器——未找到对应的ModbusTCP配置！");
                return null;
            }
            //2、根据配置信息拿到对应的ip和端口
            var ip = modbustcp.IP;
            var port = modbustcp.Port;
            //3、根据ip和端口建立对应的plc连接
            try
            {
                using (var client = new TcpClient(ip, port))
                {
                    var factory = new ModbusFactory();
                    var master = factory.CreateMaster(client);

                    ushort startAddress = modbustcp.StartAddress;
                    ushort numRegisters = modbustcp.Num; // 读取的寄存器数量

                    var result = await master.ReadHoldingRegistersAsync(modbustcp.SlaveID, startAddress, numRegisters);
                    return result;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "读取保持寄存器——读取线圈时发生错误！");
                return null;
            }
        }




        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="isExist"></param>
        /// <param name="modbusTCPConfig"></param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(ModbusTCPConfig isExist, ModbusTCPConfig modbusTCPConfig)
        {
            isExist.ProxyName = modbusTCPConfig.ProxyName;
            isExist.IP = modbusTCPConfig.IP;
            isExist.Port = modbusTCPConfig.Port;
            isExist.SlaveID = modbusTCPConfig.SlaveID;
            isExist.FunctionCode = modbusTCPConfig.FunctionCode;
            isExist.IsOpen = modbusTCPConfig.IsOpen;
            isExist.StartAddress = modbusTCPConfig.StartAddress;
            isExist.Num = modbusTCPConfig.Num;
            isExist.LastModified = DateTime.Now;

            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 写线圈
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> WriteSingCoilsAsync(int id, bool value)
        {
            //1、根据id先判断是否存在
            var modbustcp = _dbContext.modbusTCPConfigs.FirstOrDefault(x => x.Id == id);
            if (modbustcp == null)
            {
                _logger.LogError("写线圈——未找到对应的ModbusTCP配置！");
                return false;
            }
            //2、根据配置信息拿到对应的ip和端口
            var ip = modbustcp.IP;
            var port = modbustcp.Port;
            //3、根据ip和端口建立对应的plc连接
            try
            {
                using (var client = new TcpClient(ip, port))
                {
                    var factory = new ModbusFactory();
                    var master = factory.CreateMaster(client);

                    ushort startAddress = modbustcp.StartAddress;
                    ushort numRegisters = modbustcp.Num; // 读取的寄存器数量

                    await master.WriteSingleCoilAsync(modbustcp.SlaveID,startAddress,value);
                    return true;
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "写线圈——读取线圈时发生错误！");
                return false;
            }
        }



        public async Task<bool> ExistsByProxyName(string proxyName)
        {
            return await _dbContext.modbusTCPConfigs
                .AnyAsync(c => c.ProxyName == proxyName);
        }

        public async Task BulkUpsertAsync(IEnumerable<ModbusTCPExcel> configs)
        {
            foreach (var config in configs)
            {
                var existing = await _dbContext.modbusTCPConfigs
                    .FirstOrDefaultAsync(c => c.ProxyName == config.ProxyName);

                if (existing != null)
                {
                    // 更新逻辑
                    existing.IP = config.IP;
                    existing.Port = config.Port;
                    existing.SlaveID = config.SlaveID;
                    existing.FunctionCode = config.FunctionCode;
                    existing.StartAddress = config.StartAddress;
                    existing.Num = config.Num;
                    existing.LastModified = DateTime.Now;

                }
                else
                {
                    // 新增逻辑
                    var newConfig = new ModbusTCPConfig
                    {
                        ProxyName = config.ProxyName,
                        IP = config.IP,
                        Port = config.Port,
                        SlaveID = config.SlaveID,
                        FunctionCode = config.FunctionCode,
                        StartAddress = config.StartAddress,
                        Num = config.Num,
                        IsOpen = true,
                        IsDeleted = false,
                        Createtime = DateTime.Now,
                        LastModified = DateTime.Now

                    };
                    await _dbContext.modbusTCPConfigs.AddAsync(newConfig);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
