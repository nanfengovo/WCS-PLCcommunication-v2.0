using Microsoft.Extensions.Logging;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTasks.ModbusTcp
{
    public class ReadJob 
    {
        public readonly MyDbContext _dbContext;

        public readonly ILogger<ReadJob> _logger;

        public ReadJob(MyDbContext dbContext, ILogger<ReadJob> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        
    }
}
