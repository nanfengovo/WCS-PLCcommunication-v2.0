using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.BcakServiceModel
{
    // 连接信息实体
    public class PlcConnectionInfo
    {
        public string Key { get; set; }
        public bool IsConnected { get; set; }
        public DateTime LastUsedTime { get; set; }
        public int UsageCount { get; set; }
    }
}
