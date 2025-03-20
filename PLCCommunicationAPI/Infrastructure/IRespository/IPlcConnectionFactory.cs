using PLCCommunication_Model.BcakServiceModel;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IRespository
{
    // PLC连接工厂接口
    public interface IPlcConnectionFactory
    {
        Plc GetConnection(string ip, int port);
        void ReleaseConnection(Plc plc);
        IEnumerable<PlcConnectionInfo> GetConnectionStatus();
    }
}
