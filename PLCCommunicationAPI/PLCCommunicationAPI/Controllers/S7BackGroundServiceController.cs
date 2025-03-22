using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;

namespace PLCCommunication_API.Controllers
{
    /// <summary>
    /// S7BackGroundServiceController中是关于和S7后台任务相关的
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.S7BackGroundService))]
    public class S7BackGroundServiceController : ControllerBase
    {
        private readonly ILogger<S7BackGroundServiceController> _logger;

        private readonly IS7ReadTaskResposity _s7ReadTaskResposity;

        public S7BackGroundServiceController(ILogger<S7BackGroundServiceController> logger, IS7ReadTaskResposity s7ReadTaskResposity)
        {
            _logger = logger;
            _s7ReadTaskResposity = s7ReadTaskResposity;
        }

        /// <summary>
        /// 获取所有的S7任务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result> GetAllTask()
        {
            var data = await _s7ReadTaskResposity.FindAllAsync();
            return new Result { Code = 200, Data = data, Msg = "查询成功！" };
        }


        /// <summary>
        /// 删除线程
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<Result> DelById(int id)
        {
            var result = await _s7ReadTaskResposity.DeleteAsync(id);
            if (result)
            {
                return new Result { Code = 200, Data = result, Msg = "删除成功！" };
            }
            else
            {
                return new Result { Code = 400, Data = result, Msg = "删除失败！" };
            }

        }


        /// <summary>
        /// 添加S7任务
        /// </summary>
        /// <param name="s7Task"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> AddS7Task([FromForm] S7ReadTask s7Task)
        {
            var result = await _s7ReadTaskResposity.AddAsync(s7Task);
            if (result)
            {
                return new Result { Code = 200, Data = result, Msg = "添加成功！" };
            }
            else
            {
                return new Result { Code = 400, Data = result, Msg = "添加失败！" };
            }
        }


        /// <summary>
        /// 修改任务状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> ChangeS7TaskStatus(int id)
        {
            var result = await _s7ReadTaskResposity.ModifystatusS7TaskByid(id);
            if (result)
            {
                return new Result { Code = 200, Data = result, Msg = "状态修改成功！" };
            }
            else
            {
                return new Result { Code = 400, Data = result, Msg = "状态修改失败！" };
            }
        }

        /// <summary>
        /// 修改任务
        /// </summary>
        /// <param name="s7task"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<Result> EditS7Task(int id, [FromForm]S7TaskDTO s7task)
        {
            //根据id找到需要修改的对象
            var isExist = await _s7ReadTaskResposity.FindEntityByIdAsync(id);
            if (isExist == null)
            {
                _logger.LogWarning($"需要修改的id为{id}的对象不存在！！请检查传入的id");
                return new Result { Code = 404, Msg = $"需要修改的id为{id}的对象不存在！！请检查传入的id" };
            }
            //var task = new S7ReadTask
            //{
            //    Name = s7task.Name,
            //    IP = s7task.IP,
            //    Port = s7task.Port,
            //    DBaddr = s7task.DBaddr,
            //    IsOpen = s7task.IsOpen,
            //    LastModified = DateAndTime.Now
            //};

            var result = await _s7ReadTaskResposity.EditAsync(isExist, s7task);
            if (result)
            {
                return new Result { Code = 200, Data = result, Msg = "修改成功！" };
            }
            else
            {
                return new Result { Code = 400, Data = result, Msg = "修改失败！" };
            }
        }
    }
}
