using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PLCCommunicationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [HttpGet]
        public string GetTestEnvironment()
        {
            return Environment.GetEnvironmentVariable("test");
        }
    }
}
