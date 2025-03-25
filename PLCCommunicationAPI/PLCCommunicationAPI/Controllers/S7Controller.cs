using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.Configs;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;
using System.Data;

namespace PLCCommunication_API.Controllers
{
    /// <summary>
    /// S7Controller用来处理和S7协议相关的
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.S7Config))]
    [Authorize]
    public class S7Controller : ControllerBase
    {
        private readonly ILogger<S7Controller> _logger;

        private readonly IS7ConfigService _s7ConfigService;

        public S7Controller(ILogger<S7Controller> logger, IS7ConfigService s7ConfigService)
        {
            _logger = logger;
            _s7ConfigService = s7ConfigService;
        }

        /// <summary>
        /// 新增S7配置
        /// </summary>
        /// <param name="s7ConfigDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> AddS7Config([FromForm] S7ConfigDTO s7ConfigDTO)
        {
            //DTO转实体
            var s7Config = new S7Config
            {
                ProxyName = s7ConfigDTO.ProxyName,
                IP = s7ConfigDTO.IP,
                Port = s7ConfigDTO.Port,
                DBID = s7ConfigDTO.DBID,
                Address= s7ConfigDTO.Address,
                Type = s7ConfigDTO.Type,
                Length = s7ConfigDTO.Length,
                bit = s7ConfigDTO.bit,
                Remark = s7ConfigDTO.Remark,
                IsOpen = s7ConfigDTO.IsOpen,
                Createtime = DateTime.Now,
                LastModified = DateTime.Now,
            };
            //判断是否存在这个配置名
            var isExist = await _s7ConfigService.FindEntityByNameAsync(s7ConfigDTO.ProxyName);
            if(isExist)
            {
                _logger.LogWarning($"新增S7配置-将要新增的配置名{s7ConfigDTO.ProxyName}已存在！！配置不能重名！");
                return new Result { Code = 405, Msg = $"将要新增的配置名{s7ConfigDTO.ProxyName}已存在！！配置不能重名！" };
            }
            var result = await _s7ConfigService.CreateAsync(s7Config);
            if(!result)
            {
                _logger.LogWarning($"新增S7配置，新增配置名为：{s7Config.ProxyName}的配置失败！");
                return new Result { Code = 407, Msg = $"新增配置名为：{s7Config.ProxyName}的配置失败！" };
            }
            return new Result { Code = 200, Msg = $"新增配置名为：{s7Config.ProxyName}的配置成功！" };
        }

        /// <summary>
        /// 获取所有的S7配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<S7Config>> GetAllS7Configs()
        {
            _logger.LogInformation($"获取所有的S7配置-获取一次所有的S7配置{DateAndTime.Now}");
            return await _s7ConfigService.FindAllAsync();
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Result> DeleteById(List<int> ids)
        {
            foreach (var id in ids)
            {
                var isExist = await _s7ConfigService.FindEntityByIdAsync(id);
                if (isExist == null)
                {
                    _logger.LogWarning($"根据id删除-删除失败，不存在id为{id}的S7配置");
                    // 如果您希望一旦遇到不存在的 ID 就返回错误，可以在这里返回
                    // return new Result { Code = 404, Msg = $"根据id删除-删除失败，不存在id为{id}的S7配置" };
                    continue; // 否则，继续处理下一个 ID
                }
                if(isExist.IsOpen)
                {
                    _logger.LogWarning($"根据id删除-删除配置名为{isExist.ProxyName}的S7配置失败！，因为还在启动状态！");
                    //return new Result { Code = 408, Msg = $"删除配置名为{isExist.ProxyName}的S7配置失败！，因为还在启动状态！" };
                    continue; // 否则，继续处理下一个 ID
                }
                var result = await _s7ConfigService.DeletedAsync(isExist);
                if (!result)
                {
                    _logger.LogWarning($"根据id删除-删除配置名为{isExist.ProxyName}的S7配置失败！");
                    // 如果您希望一旦删除失败就返回错误，可以在这里返回
                    // return new Result { Code = 407, Msg = $"删除配置名为{isExist.ProxyName}的S7配置失败！" };
                    continue; // 否则，继续处理下一个 ID
                }
                _logger.LogInformation($"删除配置名为{isExist.ProxyName}的S7配置成功！");
            }

            // 如果所有 ID 都处理成功，返回成功结果
            return new Result { Code = 200, Msg = "所有指定的S7配置删除成功！" };
        }

        /// <summary>
        /// 启用配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> Enable(int id)
        {
            var isExist = await _s7ConfigService.FindEntityByIdAsync(id);
            if (isExist == null)
            {
                _logger.LogWarning($"id为{id}的S7配置不存在！");
                return new Result { Code = 404, Msg = $"id为{id}的S7配置不存在！" };
            }
            if(isExist.IsDeleted)
            {
                _logger.LogWarning($"id为{id}的S7配置不存在！");
                return new Result { Code = 404, Msg = $"id为{id}的S7配置不存在！" };
            }
            
            //根据id查当前的状态
            //var state = await _s7ConfigService.Getstate(id);
            if (isExist.IsOpen)
            {
                _logger.LogWarning($"启用配置-启用id为{id}的S7配置失败！，因为还在启动状态！");
                return new Result { Code = 408, Msg = $"启用id为{id}的S7配置失败！，因为还在启动状态！" };
            }
            var result = await _s7ConfigService.Enable(id);
            if (!result)
            {
                _logger.LogWarning($"启用配置-启用id为{id}的S7配置失败！");
                return new Result { Code = 407, Msg = $"启用id为{id}的S7配置失败！" };
            }
            _logger.LogInformation($"启用id为{id}的配置成功！");
            return new Result { Code = 200, Msg = $"启用id为{id}的配置成功！" };
        }

        /// <summary>
        /// 禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> Disable(int id)
        {
            var isExist = await _s7ConfigService.FindEntityByIdAsync(id);
            if (isExist == null)
            {
                _logger.LogWarning($"id为{id}的S7配置不存在！");
                return new Result { Code = 404, Msg = $"id为{id}的S7配置不存在！" };
            }
            if (isExist.IsDeleted)
            {
                _logger.LogWarning($"id为{id}的S7配置不存在！");
                return new Result { Code = 404, Msg = $"id为{id}的S7配置不存在！" };
            }

            //根据id查当前的状态
            //var state = await _s7ConfigService.Getstate(id);
            if (!isExist.IsOpen)
            {
                _logger.LogWarning($"禁用配置-禁用id为{id}的S7配置失败！，因为已经是禁用状态！");
                return new Result { Code = 408, Msg = $"禁用id为{id}的S7配置失败！，因为已经是禁用状态！" };
            }
            var result = await _s7ConfigService.Disable(id);
            if (!result)
            {
                _logger.LogWarning($"禁用配置-禁用id为{id}的S7配置失败！");
                return new Result { Code = 407, Msg = $"禁用id为{id}的S7配置失败！" };
            }
            _logger.LogInformation($"禁用id为{id}的配置成功！");
            return new Result { Code = 200, Msg = $"禁用id为{id}的配置成功！" };
        }

        /// <summary>
        /// 编辑/修改S7数据点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="s7ConfigDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> EditS7ById(int id, [FromForm] S7ConfigDTO s7ConfigDTO)
        {
            //根据id找到需要修改的对象
            var isExist = await _s7ConfigService.FindEntityByIdAsync(id);
            if (isExist == null)
            {
                _logger.LogWarning($"需要修改的id为{id}的对象不存在！！请检查传入的id");
                return new Result { Code = 404, Msg = $"需要修改的id为{id}的对象不存在！！请检查传入的id" };
            }

            //DTO转实体
            var s7Config = new S7Config
            {
                ProxyName = s7ConfigDTO.ProxyName,
                IP = s7ConfigDTO.IP,
                Port = s7ConfigDTO.Port,
                DBID = s7ConfigDTO.DBID,
                Address = s7ConfigDTO.Address,
                Type = s7ConfigDTO.Type,
                Length = s7ConfigDTO.Length,
                bit = s7ConfigDTO.bit,
                Remark = s7ConfigDTO.Remark,
                IsOpen = s7ConfigDTO.IsOpen,
                LastModified = DateTime.Now,
            };

            //赋值完成修改
            var result = await _s7ConfigService.UpdateAsync(isExist,s7Config);
            return new Result { Code = 200, Msg = "修改成功！" };
        }


        /// <summary>
        /// 导入S7配置
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(50_000_000)] // 50MB限制
        public async Task<Result> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new Result { Code = 400, Msg = "文件不能为空" };

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                return new Result { Code = 400, Msg = "文件格式不正确" };

            var result = await _s7ConfigService.ImportConfigsAsync(file);

            //处理上传的逻辑
            if(result.SuccessCount == result.TotalCount)
            {
                return new Result { Code = 200, Msg = "全部导入成功！" };
            }
            else if(result.SuccessCount != 0 && result.ErrorMessages.Count > 0)
            {
                return new Result { Code = 201, Msg = $"部分导入成功！导入失败的条数为{result.ErrorMessages.Count}"};
            }
            else
            {
                return new Result { Code = 400, Msg = "导入失败！" };
            }

                
        }

        /// <summary>
        /// 导出S7配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var stream = await _s7ConfigService.ExportConfigsAsync();
            return File(stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"S7Configs_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
        }

    }



}
