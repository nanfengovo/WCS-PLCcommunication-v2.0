using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class S7ProxyToTask
    {
        private readonly MyDbContext _myDbContext;

        public S7ProxyToTask(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<bool> InitializeDatabaseAsync()
        {
            var s7config = await _myDbContext.s7Configs.ToListAsync();
            foreach (var item in s7config)
            {
                //int、short -> DBD, bool -> DBX
                if (item.Type == "int" || item.Type == "short")
                {
                    var DBaddr = "DB" + item.DBID + ".DBD" + item.Address;
                    S7ReadTask s7ReadTask = new S7ReadTask
                    {
                        Name = item.ProxyName,
                        IP = item.IP,
                        Port = item.Port,
                        DBaddr = DBaddr,
                        IsOpen = true,
                        IsDeleted = false,
                        Createtime = DateTime.Now,
                        LastModified = DateTime.Now
                    };
                    var isExist = await _myDbContext.s7ReadTasks.FirstOrDefaultAsync(x => x.Name == item.ProxyName);
                    if(isExist == null)
                    {
                        _myDbContext.s7ReadTasks.Add(s7ReadTask);
                    }
                    
                }
                else if (item.Type == "bool")
                {
                    var DBaddr = "DB" + item.DBID + ".DBX" + item.Address;
                    S7ReadTask s7ReadTask = new S7ReadTask
                    {
                        Name = item.ProxyName,
                        DBaddr = DBaddr,
                        IP = item.IP,
                        Port = item.Port,
                        IsOpen = true,
                        IsDeleted = false,
                        Createtime = DateTime.Now,
                        LastModified = DateTime.Now
                    };
                    var isExist = await _myDbContext.s7ReadTasks.FirstOrDefaultAsync(x => x.Name == item.ProxyName);
                    if (isExist == null)
                    {
                        _myDbContext.s7ReadTasks.Add(s7ReadTask);
                    }

                    
                }
                else
                {
                    continue;
                }
            }
            return await _myDbContext.SaveChangesAsync() > 0;
        }
    }
}
