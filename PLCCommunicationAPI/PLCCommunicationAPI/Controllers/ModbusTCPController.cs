using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;
using System.Data;

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
        public async Task<Result> CreateModbusTCPConfig([FromForm]ModbusTCPConfigDTO modbusTCP)
        {
            var modbus = new ModbusTCPConfig()
            {
                ProxyName = modbusTCP.ProxyName,
                IP = modbusTCP.IP,
                Port = modbusTCP.Port,
                SlaveID = modbusTCP.SlaveID,
                FunctionCode = modbusTCP.FunctionCode,
                StartAddress = modbusTCP.StartAddress,
                Num = modbusTCP.Num,
                IsOpen = modbusTCP.IsOpen,
                IsDeleted = false,
                Createtime = DateAndTime.Now,
                LastModified = DateAndTime.Now,
            };
            bool res = await _modbusTCPConfigService.CreateAsync(modbus);
            if (res)
            {
                return new Result { Code = 200, Msg = "新建配置成功！" };
            }
            return new Result { Code = 400, Msg = "创建失败！！！" };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Result> DeletedById(List<int> ids)
        {
            var result = await _modbusTCPConfigService.DeletedById(ids);
            if (result)
            {
                return new Result { Code = 200, Msg = "删除成功！" };
            }
            else
            {
                return new Result { Code = 404, Msg = "删除失败！" };
            }
        }
    }
}
