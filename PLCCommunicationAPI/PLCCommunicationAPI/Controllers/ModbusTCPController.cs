using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;

namespace PLCCommunication_API.Controllers
{
    /// <summary>
    /// ModbusTCPController中是关于和支持MobusTCP协议通讯的
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.ModbusTCPConfig))]
    public class ModbusTCPController : ControllerBase
    {
        private readonly IModbusTCPConfigService _modbusTCPConfigService;

        public ModbusTCPController(IModbusTCPConfigService modbusTCPConfigService)
        {
            _modbusTCPConfigService = modbusTCPConfigService;
        }



        /// <summary>
        /// 创建ModbusTCP配置控制器
        /// </summary>
        /// <param name="modbusTCPConfig">The Modbus TCP configuration to create.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPost]
        public async Task<Result> CreateModbusTCPConfig(ModbusTCPConfig modbusTCPConfig)
        {
            bool res = await _modbusTCPConfigService.CreateAsync(modbusTCPConfig);
            if (res)
            {
                return new Result { Code = 200, Msg = "新建配置成功！" };
            }
            return new Result { Code = 400, Msg = "创建失败！！！" };
        }
    }
}
