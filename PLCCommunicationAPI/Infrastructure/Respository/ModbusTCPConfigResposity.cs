using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class ModbusTCPConfigResposity:BaseRespository<ModbusTCPConfig>,IModbusTCPConfigResposity
    {
        private readonly MyDbContext _dbContext;

        public ModbusTCPConfigResposity(MyDbContext dbContext)
        {
            base._ctx = dbContext;
            _dbContext = dbContext;
        }

        public override async Task<List<ModbusTCPConfig>> FindAllAsync()
        {
            return await _dbContext.modbusTCPConfigs.Where(x=>x.FunctionCode==1).ToListAsync();
        }

        public override Task<List<ModbusTCPConfig>> FindAllAsync(Expression<Func<ModbusTCPConfig, bool>> del)
        {
            return base.FindAllAsync(del);
        }

        public override Task<ModbusTCPConfig> FindEntityByAsync(Expression<Func<ModbusTCPConfig, bool>> del)
        {
            return base.FindEntityByAsync(del);
        }

        public override Task<ModbusTCPConfig> FindEntityByIdAsync(Guid id)
        {
            return base.FindEntityByIdAsync(id);
        }
    }
}
