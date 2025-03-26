using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.MiniExcelHelper;
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
        public Task<bool> EditAsync(S7ReadTask isExit, S7TaskDTO s7task);
        Task<ImportResult> ImportConfigsAsync(IFormFile file);
        Task<Stream> ExportConfigsAsync();
    }
}
