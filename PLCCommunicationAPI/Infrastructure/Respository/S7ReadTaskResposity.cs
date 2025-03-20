using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class S7ReadTaskResposity : BaseRespository<S7Config>, IS7ReadTaskResposity
    {
       

        private readonly MyDbContext _myDbContext;

        private readonly ILogger<S7ReadTaskResposity> _logger;

        private readonly IPlcConnectionFactory _plcConnectionFactory;

        public S7ReadTaskResposity(MyDbContext myDbContext, ILogger<S7ReadTaskResposity> logger, IPlcConnectionFactory plcConnectionFactory)
        {
            base._ctx = myDbContext;
            _myDbContext = myDbContext;
            _logger = logger;
            _plcConnectionFactory = plcConnectionFactory;
        }



        public Task<bool> CreateAsync(S7ReadTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletedAsync(S7ReadTask entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Disable(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Enable(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<S7ReadTask>> FindAllAsync(Expression<Func<S7ReadTask, bool>> del)
        {
            throw new NotImplementedException();
        }

        public Task<S7ReadTask> FindEntityByAsync(Expression<Func<S7ReadTask, bool>> del)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FindEntityByNameAsync(string proxyName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Getstate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Read(string Nmae)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(S7ReadTask isExist, S7ReadTask s7ReadTask)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(S7ReadTask entity)
        {
            throw new NotImplementedException();
        }

        Task<List<S7ReadTask>> IBaseRespository<S7ReadTask>.FindAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<S7ReadTask> IBaseRespository<S7ReadTask>.FindEntityByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<S7ReadTask> IBaseRespository<S7ReadTask>.FindEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<IActionResult> ReadAsync()
        {
            try
            {
                // 优化1：直接过滤无效任务，减少内存中的数据量
                var activeS7Tasks = await _myDbContext.s7ReadTasks
                    .Where(task => !task.IsDeleted && task.IsOpen)
                    .ToListAsync();

                if (!activeS7Tasks.Any())
                {
                    _logger.LogInformation("没有可执行的S7任务");
                    return new OkObjectResult("没有可执行的任务");
                }

                bool hasExecuted = false;

                foreach (var task in activeS7Tasks)
                {
                    try
                    {
                        // 优化2：提取到独立方法，避免嵌套过深
                        await ProcessS7TaskAsync(task);
                        hasExecuted = true;
                    }
                    catch (Exception ex)
                    {
                        // 优化3：记录错误但继续执行其他任务
                        _logger.LogError(ex, $"处理任务 {task.Name} 时发生错误");
                    }
                }

                return hasExecuted
                    ? new OkObjectResult("任务执行完成")
                    : new OkObjectResult("所有任务均失败");
            }
            catch (Exception ex)
            {
                // 优化4：全局异常捕获
                _logger.LogError(ex, "读取S7任务时发生全局错误");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task ProcessS7TaskAsync(S7ReadTask task)
        {
            var plc = _plcConnectionFactory.GetConnection(task.IP, task.Port);

            try
            {
                _logger.LogInformation("使用现有连接执行任务：{TaskName}", task.Name);

                var result = await plc.ReadAsync(task.DBaddr);
                if (result == null)
                {
                    _logger.LogWarning("从PLC {TaskName} 读取到空数据", task.Name);
                    return;
                }

                _logger.LogInformation("DB块读取成功：{TaskName}，结果：{Result}",
                    task.Name, result);
            }
            finally
            {
                _plcConnectionFactory.ReleaseConnection(plc);
            }
        }




    }
}
