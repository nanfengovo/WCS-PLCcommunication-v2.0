using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.Enum;

namespace PLCCommunicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.Test))]
    public class TestController : ControllerBase
    {
        private readonly MyDbContext _ctx ;

        public TestController(MyDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public string GetTestEnvironment()
        {
            return Environment.GetEnvironmentVariable("test");
        }

        [HttpGet]
        public List<ModbusTCPConfig> GetAllConfig()
        {
            return _ctx.modbusTCPConfigs.ToList();
        }
    }
}
