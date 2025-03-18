using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IRespository
{
    public interface IS7ReadWriteResposity : IBaseRespository<S7Config>
    {
       public  Task<object> Read(int id);
       public  Task<object> Write(int id,string input);
    }
}
