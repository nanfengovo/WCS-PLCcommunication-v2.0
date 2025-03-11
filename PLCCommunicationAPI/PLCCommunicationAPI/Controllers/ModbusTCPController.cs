using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Model.DTO;
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

        private readonly IMapper _mapper;
        public ModbusTCPController(IModbusTCPConfigService modbusTCPConfigService, IMapper mapper)
        {
            _modbusTCPConfigService = modbusTCPConfigService;
            _mapper = mapper;
        }


        /// <summary>
        /// 获取所有的Modbus配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<Result> GetAllConfig()
        {
            var data = await _modbusTCPConfigService.FindAllAsync();
            return new Result { Code = 200, Data = data, Msg = "查询成功！" };
        }

        /// <summary>
        /// 新建ModbusTCP配置
        /// </summary>
        /// <param name="modbusTCPConfig">The Modbus TCP configuration to create.</param>
        /// <returns>A result indicating success or failure.</returns>
        [HttpPost]
        [Authorize]
        public async Task<Result> CreateModbusTCPConfig(string proxyName, string ip,int port,byte SlaveID,byte FunctionCode,ushort StartAddress,ushort Num)
        {
            ModbusTCPConfig modbusTCPConfig = new ModbusTCPConfig
            {
                ProxyName = proxyName,
                IP = ip,
                Port = port,
                FunctionCode = FunctionCode,
                StartAddress = StartAddress,
                Num = Num
            };

            bool res = await _modbusTCPConfigService.CreateAsync(modbusTCPConfig);
            // 使用 AutoMapper 将实体对象映射到 DTO
            ModbusTCPConfigDTO modbusTCPConfigDTO = _mapper.Map<ModbusTCPConfigDTO>(modbusTCPConfig);
            if (res)
            {
                return new Result { Code = 200, Msg = "新建配置成功！" };
            }
            return new Result { Code = 400, Msg = "创建失败！！！" };
        }
    }
}
