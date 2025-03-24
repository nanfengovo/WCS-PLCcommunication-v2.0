using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniExcelDemo.Model;
using MiniExcelLibs;
using PLCCommunication_Model.MiniExcelModel;

namespace MiniExcelDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        [HttpPost("import")]
        public IActionResult ImportExcel(IFormFile excel)
        {
            //内存流
            var stream = new MemoryStream();
            //将excel文件复制到内存流
            excel.CopyTo(stream);
            //在内存中查询Person并转换为集合
            var list = stream.Query<S7Excel>().ToList();
            foreach (var s7 in list)
            {
                Console.WriteLine(s7.ProxyName);
                Console.WriteLine(s7.IP);
                Console.WriteLine(s7.Port);
                Console.WriteLine(s7.DBID);
                Console.WriteLine(s7.Address);
                Console.WriteLine(s7.Type);
                Console.WriteLine(s7.Length);
                Console.WriteLine(s7.bit);
                Console.WriteLine(s7.Remark);
            }
            return Ok("File import successfully");
        }



        [HttpGet("export")]
        public IActionResult ExportExcel()
        {
            //values 是导出的数据
            var values = new[]
            {
                new { Column1 = "MiniExcel", Column2 = 1 },
                new { Column1 = "Github", Column2 = 2}
            };
            //内存流
            var memoryStream = new MemoryStream();
            //将数据保存到内存流
            memoryStream.SaveAs(values);
            //设置流中的读取位置
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "demo.xlsx"
            };
        }
    }
}
