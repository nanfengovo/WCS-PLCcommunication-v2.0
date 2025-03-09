using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_Utility.Enum;

namespace PLCCommunicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.SysMenu))]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public string GetTestEnvironment()
        {
            return Environment.GetEnvironmentVariable("test");
        }
    }
}
