using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.MiniExcelModel;
using PLCCommunication_Utility.MiniExcelHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.Service
{
    public class ModbusTCPConfigService : BaseService<ModbusTCPConfig>, IModbusTCPConfigService
    {
        private readonly IModbusTCPConfigResposity _modbusTCPConfigResposity;

        private readonly ILogger<ModbusTCPConfigService> _logger;

        public ModbusTCPConfigService(IModbusTCPConfigResposity modbusTCPConfigResposity, ILogger<ModbusTCPConfigService> logger)
        {
            base._respository = modbusTCPConfigResposity;
            _modbusTCPConfigResposity = modbusTCPConfigResposity;
            _logger = logger;
        }


        public async Task<List<ModbusTCPConfig>> GetAll()
        {
            return await base._respository.FindAllAsync();
        }

        /// <summary>
        /// 创建ModbusTCP配置
        /// </summary>
        /// <param name="modbusTCPConfig"></param>
        /// <returns></returns>
        public async Task<bool> Create(ModbusTCPConfig modbusTCPConfig)
        {
            return await base._respository.CreateAsync(modbusTCPConfig);
        }

        public async Task<bool> DeletedById(List<int> ids)
        {
            return await _modbusTCPConfigResposity.DeletedByIdAsync(ids);
        }

        public async Task<bool> UpdateAsync(ModbusTCPConfig isExist, ModbusTCPConfig modbusTCPConfig)
        {
            return await _modbusTCPConfigResposity.UpdateAsync(isExist,modbusTCPConfig);
        }

        public Task<bool[]> ReadCoilsAsync(int id)
        {
            return _modbusTCPConfigResposity.ReadCoilsAsync(id);
        }

        public async Task<bool[]> ReadDiscreteInputsAsync(int id)
        {
            return await _modbusTCPConfigResposity.ReadDiscreteInputsAsync(id);
        }

        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ushort[]> ReadHoldingRegistersAsync(int id)
        {
            return await _modbusTCPConfigResposity.ReadHoldingRegistersAsync(id);
        }

        /// <summary>
        /// 写入线圈
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> WriteSingCoilsAsync(int id, bool value)
        {
            return await _modbusTCPConfigResposity.WriteSingCoilsAsync(id, value);
        }



        /// <summary>
        /// 导入Modbus配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<ImportResult> ImportConfigsAsync(IFormFile file)
        {
            var result = new ImportResult();

            try
            {
                var stream = new MemoryStream();
                file.CopyTo(stream);
                var configs = stream.Query<ModbusTCPExcel>().ToList();
                result.TotalCount = configs.Count;

                var errors = new List<string>();
                var validConfigs = new List<ModbusTCPExcel>();

                // 数据验证
                for (int i = 0; i < configs.Count; i++)
                {
                    var config = configs[i];
                    var rowNumber = i + 2; // Excel行号从第2行开始

                    try
                    {
                        // 基本验证
                        if (string.IsNullOrWhiteSpace(config.ProxyName))
                            throw new Exception("配置名不能为空");
                        _logger.LogError("导入Modbus配置文件——配置名不能为空");

                        if (!IPAddress.TryParse(config.IP, out _))
                            throw new Exception("IP地址格式无效");
                        _logger.LogError("导入Modbus配置文件——IP地址格式无效");

                        if (config.Port < 1 || config.Port > 65535)
                            throw new Exception("端口号无效");
                        _logger.LogError("导入Modbus配置文件——端口号无效");

                        // 业务验证
                        if (await _modbusTCPConfigResposity.ExistsByProxyName(config.ProxyName))
                            throw new Exception("配置名已存在");
                        _logger.LogError("导入Modbus配置文件——配置名已存在");

                        validConfigs.Add(config);
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"第{rowNumber}行错误: {ex.Message}");
                    }
                }

                // 批量操作
                if (validConfigs.Any())
                {
                    await _modbusTCPConfigResposity.BulkUpsertAsync(validConfigs);
                    result.SuccessCount = validConfigs.Count;
                }

                result.ErrorMessages = errors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Modbus配置导入失败");
                result.ErrorMessages.Add($"系统错误: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public async Task<Stream> ExportConfigsAsync()
        {
            var configs = await _modbusTCPConfigResposity.GetAllAsync();
            var stream = new MemoryStream();
            // 修改后：添加映射步骤
            var excelModels = configs.Select(config => MapToExcelModel(config)).ToList();
            MiniExcel.SaveAs(stream, excelModels);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }


        private object MapToExcelModel(ModbusTCPConfig config)
        {
            return new ModbusTCPExcel
            {
                ProxyName = config.ProxyName,
                IP = config.IP,
                Port = config.Port,
                SlaveID = config.SlaveID,
                FunctionCode = config.FunctionCode,
                StartAddress = config.StartAddress,
                Num = config.Num,

            };
        }
    }
}
