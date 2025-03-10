using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.APIHelper
{
    public class Result
    {
        /// <summary>
        /// 返回给前端的代码
        /// </summary>
        public int  Code { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string? Msg { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }
    }
}
