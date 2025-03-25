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

        private readonly ILogger<ModbusTCPController> _logger;
        public ModbusTCPController(IModbusTCPConfigService modbusTCPConfigService, IMapper mapper, ILogger<ModbusTCPController> logger)
        {
            _modbusTCPConfigService = modbusTCPConfigService;
            _mapper = mapper;
            _logger = logger;
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


        /// <summary>
        /// 编辑/修改ModbusTCP数据点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ModbusTCPConfigDTO"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> EditModbusTCPConfigById(int id, [FromForm] ModbusTCPConfigDTO modbusTCPConfigDTO)
        {
            //根据id找到需要修改的对象
            var isExist = await _modbusTCPConfigService.FindEntityByIdAsync(id);
            if (isExist == null)
            {
               _logger.LogWarning($"需要修改的id为{id}的对象不存在！！请检查传入的id");
                return new Result { Code = 404, Msg = $"需要修改的id为{id}的对象不存在！！请检查传入的id" };
            }
            if(isExist.IsOpen)
            {
                _logger.LogWarning($"需要修改的id为{id}的对象是启用状态不能修改！！请检查传入的id");
                return new Result { Code = 408, Msg = $"需要修改的id为{id}的对象是启用状态不能修改！！请先禁用" };
            }
            //DTO转实体
            var modbus = new ModbusTCPConfig
            {
                ProxyName = modbusTCPConfigDTO.ProxyName,
                IP = modbusTCPConfigDTO.IP,
                Port = modbusTCPConfigDTO.Port,
                SlaveID = modbusTCPConfigDTO.SlaveID,
                FunctionCode = modbusTCPConfigDTO.FunctionCode,
                IsOpen = modbusTCPConfigDTO.IsOpen,
                StartAddress = modbusTCPConfigDTO.StartAddress,
                Num = modbusTCPConfigDTO.Num,
                LastModified = DateTime.Now,
            };

            //赋值完成修改
            var result = await _modbusTCPConfigService.UpdateAsync(isExist, modbus);
            return new Result { Code = 200, Msg = "修改成功！" };
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> UpdateModbusTCPConfigIsOpenById(int id)
        {
            var isExist = await _modbusTCPConfigService.FindEntityByIdAsync(id);
            if (isExist == null)
            {
                _logger.LogWarning($"需要修改的id为{id}的对象不存在！！请检查传入的id");
                return new Result { Code = 404, Msg = $"需要修改的id为{id}的对象不存在！！请检查传入的id" };
            }
            isExist.IsOpen = !isExist.IsOpen;
            var result = await _modbusTCPConfigService.UpdateAsync(isExist, isExist);
            return new Result { Code = 200, Msg = "修改成功！" };
        }

        /// <summary>
        /// 读取线圈 对应的功能码是读线圈寄存器 --01 Read Coils(0x)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> ReadCoils(int id)
        {
            var result = await _modbusTCPConfigService.ReadCoilsAsync(id);
            if(result == null)
            {
                return new Result { Code = 404, Msg = "读取失败！" };
            }
            return new Result { Code = 200, Data = result, Msg = "读取成功！" };
        }

        /// <summary>
        /// 写入线圈 对应的功能码是写线圈寄存器 --05 Write Single Coil(0x)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WriteSingleCoil(int id , bool value)
        {
            var result = await _modbusTCPConfigService.WriteSingCoilsAsync(id, value);
            if (result)
            {
                return Ok(new Result { Code = 200, Msg = "写入成功！" });
            }
            return Ok(new Result { Code = 404, Msg = "写入失败！" });
        }

        /// <summary>
        /// 读取离散寄存器 对应的功能码是读离散寄存器 --02 Read Discrete Inputs(1x)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> ReadDiscreteInputs(int id)
        {
            var result = await _modbusTCPConfigService.ReadDiscreteInputsAsync(id);
            if (result == null)
            {
                return new Result { Code = 404, Msg = "读取失败！" };
            }
            return new Result { Code = 200, Data = result, Msg = "读取成功！" };
        }

        /// <summary>
        /// 读取保持寄存器 对应的功能码是读保持寄存器 --03 Read Holding Registers(4x)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> ReadHoldingRegisters(int id)
        {
            var result = await _modbusTCPConfigService.ReadHoldingRegistersAsync(id);
            if (result == null)
            {
                return new Result { Code = 404, Msg = "读取失败！" };
            }
            return new Result { Code = 200, Data = result, Msg = "读取成功！" };
        }

        /// <summary>
        /// 导入Modbus配置
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

            var result = await _modbusTCPConfigService.ImportConfigsAsync(file);

            //处理上传的逻辑
            if (result.SuccessCount == result.TotalCount)
            {
                return new Result { Code = 200, Msg = "全部导入成功！" };
            }
            else if (result.SuccessCount != 0 && result.ErrorMessages.Count > 0)
            {
                return new Result { Code = 201, Msg = $"部分导入成功！导入失败的条数为{result.ErrorMessages.Count}" };
            }
            else
            {
                return new Result { Code = 400, Msg = "导入失败！" };
            }


        }



    }
}
