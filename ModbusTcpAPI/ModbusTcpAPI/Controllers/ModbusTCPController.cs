using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModbusTcpAPI.domainServer;
using NModbus;
using System.Net.Sockets;

namespace ModbusTcpAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModbusTCPController : ControllerBase
    {
        private readonly ModbusTCPConfig _modbusConfig;
        private readonly ModbusTCPServer _modbusTCPServer;
        private readonly ILogger<ModbusTCPController> _logger;

        public ModbusTCPController(IOptions<ModbusTCPConfig> modbusConfig, ModbusTCPServer modbusTCPServer, ILogger<ModbusTCPController> logger)
        {
            _modbusConfig = modbusConfig.Value;
            _modbusTCPServer = modbusTCPServer;
            _logger = logger;
        }

        /// <summary>
        /// 测试是否可以读取到配置文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Test()
        {
            return _modbusConfig.IP;
        }

        /// <summary>
        /// 读取线圈寄存器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ReadCoils()
        {
            var res = await _modbusTCPServer.ReadCoils();
            foreach (var item in res)
            {
                _logger.LogInformation(item.ToString());
            }
            return Ok(res);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WriteCoils(bool value)
        {
            var res = await _modbusTCPServer.WriteCoils(value);
            if (res)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 读取离散寄存器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ReadDiscreteInputs()
        {
            var res = await _modbusTCPServer.ReadDiscreteInputsAsync();
            foreach (var item in res)
            {
                _logger.LogInformation(item.ToString());
            }
            return Ok(res);
        }

        /// <summary>
        /// 读取保持寄存器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ReadHoldingRegisters()
        {

            var result = await _modbusTCPServer.ReadHoldingRegisters();
            foreach (var item in result)
            {
                _logger.LogInformation(item.ToString());
            }
            return Ok(result);

        }



        /// <summary>
        /// 写入保持寄存器
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> WriteSingleRegister([FromBody] ushort value)
        {
            var result = await _modbusTCPServer.WriteSingleRegister(value);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
