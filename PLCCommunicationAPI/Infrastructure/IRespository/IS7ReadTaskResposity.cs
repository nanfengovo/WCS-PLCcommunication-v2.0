using Microsoft.AspNetCore.Mvc;
using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IRespository
{
    public interface IS7ReadTaskResposity:IBaseRespository<S7ReadTask>
    {
        public Task<bool> FindEntityByNameAsync(string proxyName);
        public Task<bool> DeletedAsync(S7ReadTask entity);
        public Task<bool> Getstate(int id);
        public Task<bool> Enable(int id);
        public Task<bool> Disable(int id);
        public Task<bool> UpdateAsync(S7ReadTask isExist, S7ReadTask s7ReadTask);

        public  Task<IActionResult> ReadAsync();
        public Task<bool> DeleteAsync(int id);
        public Task<bool> AddAsync(S7ReadTask s7Task);
        public Task<bool> ModifystatusS7TaskByid(int id);
    }
}
