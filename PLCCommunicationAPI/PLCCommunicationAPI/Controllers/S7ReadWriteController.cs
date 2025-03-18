using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;

namespace PLCCommunication_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.S7ReadWrite))]
    [Authorize]
    public class S7ReadWriteController : ControllerBase
    {
        private readonly ILogger<S7ReadWriteController> _logger;

        private readonly IS7ReadWriteService _s7ReadWriteService;

        public S7ReadWriteController(ILogger<S7ReadWriteController> logger, IS7ReadWriteService s7ReadWriteService)
        {
            _logger = logger;
            _s7ReadWriteService = s7ReadWriteService;
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> Read(int id)
        {
            //var s7 = new S7Config
            //{
            //    IP = s7ReadWrite.IP,
            //    Port = s7ReadWrite.Port,
            //    DBID = s7ReadWrite.DBID,
            //    Address = s7ReadWrite.Address,
            //    Type = s7ReadWrite.Type,
            //    Length = s7ReadWrite.Length,
            //    bit = s7ReadWrite.bit
            //};

           var result = await _s7ReadWriteService.ReadSingleAsync(id);

            return new Result { Code = 200,Data = result };
        }


        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> Write(int id,string input)
        {
            var result = await _s7ReadWriteService.WriteSingleAsync(id,input);
            return new Result { Code = 200, Data = result };

        }
    }
}
