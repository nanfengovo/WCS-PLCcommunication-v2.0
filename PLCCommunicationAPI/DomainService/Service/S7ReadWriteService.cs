using Microsoft.Extensions.Logging;
using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.Service
{
    public class S7ReadWriteService : BaseService<S7Config>, IS7ReadWriteService
    {
        private readonly IS7ReadWriteResposity _s7ReadWriteResposity;

        private readonly ILogger<S7ConfigService> _logger;

        public S7ReadWriteService(IS7ReadWriteResposity s7ReadWriteResposity, ILogger<S7ConfigService> logger)
        {
            _s7ReadWriteResposity = s7ReadWriteResposity;
            _logger = logger;
        }

        public async Task<object> ReadSingleAsync(int id)
        {
            return await _s7ReadWriteResposity.Read(id);
        }

        public async Task<object> WriteSingleAsync(int id,string input)
        {
            return await _s7ReadWriteResposity.Write(id, input);
        }
    }
}
