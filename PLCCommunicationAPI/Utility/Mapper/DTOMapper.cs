using AutoMapper;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.Mapper
{
    public class DTOMapper:Profile  
    {
         public DTOMapper() 
        {
            base.CreateMap<ModbusTCPConfig, ModbusTCPConfigDTO>();
        }
    }
}
