using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using MiniExcelLibs.OpenXml;
using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Migrations;
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

        /// <summary>
        /// 导入S7配置
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
                var configs = stream.Query<S7Excel>().ToList();
                result.TotalCount = configs.Count;

                var errors = new List<string>();
                var validConfigs = new List<S7Excel>();

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
                        _logger.LogError("导入S7配置文件——配置名不能为空");

                        if (!IPAddress.TryParse(config.IP, out _))
                            throw new Exception("IP地址格式无效");
                        _logger.LogError("导入S7配置文件——IP地址格式无效");

                        if (config.Port < 1 || config.Port > 65535)
                            throw new Exception("端口号无效");
                        _logger.LogError("导入S7配置文件——端口号无效");

                        // 业务验证
                        if (await _s7ConfigResposity.ExistsByProxyName(config.ProxyName))
                            throw new Exception("配置名已存在");
                        _logger.LogError("导入S7配置文件——配置名已存在");

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
                    await _s7ConfigResposity.BulkUpsertAsync(validConfigs);
                    result.SuccessCount = validConfigs.Count;
                }

                result.ErrorMessages = errors;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "S7配置导入失败");
                result.ErrorMessages.Add($"系统错误: {ex.Message}");
            }

            return result;
        }

        public async Task<Stream> ExportConfigsAsync()
        {
            var configs = await _s7ConfigResposity.GetAllAsync();
            var stream = new MemoryStream();
            // 修改后：添加映射步骤
            var excelModels = configs.Select(config => MapToExcelModel(config)).ToList();
            MiniExcel.SaveAs(stream, excelModels);

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private object MapToExcelModel(S7Config config)
        {
            return new S7Excel
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
              
            };
        }

       

     
    }
}
