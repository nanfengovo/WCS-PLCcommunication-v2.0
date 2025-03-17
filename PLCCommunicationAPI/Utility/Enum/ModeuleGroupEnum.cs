using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.Enum
{
    /// <summary>
    /// 模块分组
    /// </summary>
    public enum ModeuleGroupEnum
    {
        User = 0,
        SysMenu = 1,
        Authorize = 2,
        Test = 3,
        ModbusTCPConfig = 4,
        S7Config = 5,
        S7ReadWrite = 6,

    }
}
