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
    }
}
