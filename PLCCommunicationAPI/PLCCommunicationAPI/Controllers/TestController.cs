using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;


namespace PLCCommunicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.Test))]
    public class TestController : ControllerBase
    {
        private readonly IModbusTCPConfigService _modbusTCPConfigService;

        private readonly IMapper _mapper;
        public TestController(IModbusTCPConfigService modbusTCPConfigService, IMapper mapper)
        {
            _modbusTCPConfigService = modbusTCPConfigService;
            _mapper = mapper;
        }


        [HttpGet]
        public string GetTestEnvironment()
        {
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
    }
}
