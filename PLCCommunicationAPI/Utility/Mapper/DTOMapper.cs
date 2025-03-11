using AutoMapper;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.Identity;
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
            //ModbusTCPConfig
            base.CreateMap<ModbusTCPConfig, ModbusTCPConfigDTO>();

            //User
            base.CreateMap<User, UserDTO>();
        }
    }
}
