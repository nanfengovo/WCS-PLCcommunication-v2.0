using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.BcakServiceModel
{
    // 异常类
    public class PlcConnectionException : Exception
    {
        public PlcConnectionException(string message) : base(message) { }
    }
}
