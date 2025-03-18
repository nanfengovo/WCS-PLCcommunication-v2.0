using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class S7ReadWriteResposity:BaseRespository<S7Config>,IS7ReadWriteResposity
    {
        DateTime Now = DateTime.UtcNow;
        private readonly MyDbContext _dbContext;

        //private  Plc  _plc;

        private readonly ILogger<S7ReadWriteResposity> _logger;

        public S7ReadWriteResposity (MyDbContext dbContext,ILogger<S7ReadWriteResposity> logger)
        {
            base._ctx = dbContext;
            _dbContext = dbContext;
            _logger = logger;
            //_plc = plc;
        }

        //public void Dispose()
        //{
        //    ((IDisposable)_plc).Dispose();
        //}

        /// <summary>
        ///根据id去读取 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Object> Read(int id)
        {

            var isExist = await _dbContext.s7Configs.FirstOrDefaultAsync(x => x.Id == id);

            if(isExist == null)
            {
                return false;
            }
            var ip = isExist.IP;
            var port = isExist.Port;
            short rack = 0;
            short slot = 0;
            var type = isExist.Type;
            var dbid = isExist.DBID;
            var address = isExist.Address;
            using (Plc _plc = new Plc(CpuType.S71200, ip,port, rack, slot))
            {
                //int、short -> DBD, bool -> DBX
                try
                {
                    if (!_plc.IsConnected)
                    {
                        _plc.Open();
                        _logger.LogInformation($"{Now}与配置名为：{isExist.ProxyName}的PLC建立连接:");
                        if (type == "int" || type == "short")
                        {
                            var result = await _plc.ReadAsync("DB" + dbid + ".DBD" + address);
                            if (result! == null)
                            {
                                _logger.LogInformation($"DB块读取：对配置名为：{isExist.ProxyName}DB块进行读取，结果为{result}");
                                return result;
                            }
                            return result;

                        }
                        if (type == "bool")
                        {
                            var result = await _plc.ReadAsync("DB" + dbid + ".DBX" + address);
                            if (result! == null)
                            {
                                _logger.LogInformation($"DB块读取：对配置名为：{isExist.ProxyName}DB块进行读取，结果为{result}");
                                return result;
                            }
                            return result;
                        }
                        else
                        {
                            _logger.LogWarning($"{Now}对配置名为{isExist.ProxyName}进行读取暂时不支持{type}类型！！");
                            return 404;
                        }
                    }
                    return "连接失败";
                   
                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    return ex.Message;
                }
            }
                

            

        }





        /// <summary>
        ///根据id写入S7数据点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Object> Write(int id,string input)
        {

            var isExist = await _dbContext.s7Configs.FirstOrDefaultAsync(x => x.Id == id);

            if (isExist == null)
            {
                return false;
            }
            var ip = isExist.IP;
            var port = isExist.Port;
            short rack = 0;
            short slot = 0;
            var type = isExist.Type;
            var dbid = isExist.DBID;
            var address = isExist.Address;
            using (Plc _plc = new Plc(CpuType.S71200, ip, port, rack, slot))
            {
                //int、short -> DBD, bool -> DBX
                try
                {
                    if (!_plc.IsConnected)
                    {
                        _plc.Open();
                        _logger.LogInformation($"{Now}与配置名为：{isExist.ProxyName}的PLC建立连接:");
                        if (type == "int" || type == "short")
                        {
                            try
                            {
                                await _plc.WriteAsync("DB" + dbid + ".DBD" + address, input);
                                _logger.LogInformation($"根据id写入S7数据点-对配置名为{isExist.ProxyName}的S7数据点写入{input}");
                                return true;
                            }
                            catch (Exception ex)
                            {

                                _logger.LogError($"对配置名为{isExist.ProxyName}的数据点写入失败，原因：{ex.Message}");
                            }
                        }
                        if (type == "bool")
                        {
                            try
                            {
                                await _plc.WriteAsync("DB" + dbid + ".DBX" + address, input);
                                _logger.LogInformation($"根据id写入S7数据点-对配置名为{isExist.ProxyName}的S7数据点写入{input}");
                                return true;
                            }
                            catch (Exception ex)
                            {

                                _logger.LogError($"对配置名为{isExist.ProxyName}的数据点写入失败，原因：{ex.Message}");
                            }

                        }
                        else
                        {
                            _logger.LogWarning($"{Now}对配置名为{isExist.ProxyName}进行写入暂时不支持{type}类型！！");
                            return 404;
                        }
                    }
                    return "连接失败";

                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                    return ex.Message;
                }
            }




        }
    }
}
