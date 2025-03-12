using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_API.PlugInUnit;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;


namespace PLCCommunicationAPI.Controllers
{
    /// <summary>
    /// 测试一些新加入系统的新功能
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.Test))]
    public class TestController : ControllerBase
    {
        private readonly IModbusTCPConfigService _modbusTCPConfigService;

        private readonly IMapper _mapper;

        private readonly ILogger<TestController> _logger;

        public TestController(IModbusTCPConfigService modbusTCPConfigService, IMapper mapper, ILogger<TestController> logger)
        {
            _modbusTCPConfigService = modbusTCPConfigService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [NotCheckJwtVersion]
        public string GetTestEnvironment()
        {
            _logger.LogTrace("测试日志集成");
            _logger.LogInformation("测试日志集成");
            _logger.LogDebug("测试日志集成");
            _logger.LogWarning("测试日志集成");
            _logger.LogError("测试日志集成");
            return Environment.GetEnvironmentVariable("test");
        }

        [HttpGet]
        public async Task<Result> GetAllConfig()
        {
            var data = await _modbusTCPConfigService.FindAllAsync();
            if (data.Count == 0)
            {
                return new Result { Code = 404, Msg = "当前不存在配置" };
            }

            List<ModbusTCPConfigDTO> modbusTCPConfigDTOs = new List<ModbusTCPConfigDTO>();
            
            foreach(var modbus in data)
            {
                modbusTCPConfigDTOs.Add(_mapper.Map<ModbusTCPConfigDTO>(modbus));
            }
            return new Result { Code = 200, Data = modbusTCPConfigDTOs, Msg = "查询成功！" };
        }
        
        /// <summary>
        /// 管理员才可以访问
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize("Admin")]
        public async Task<string> TestAuthorize()
        {
            return "只有管理员才可以访问";
        }



    }
}
