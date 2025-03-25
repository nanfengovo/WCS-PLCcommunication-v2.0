using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.Enum
{
    /// <summary>
    /// 状态码
    /// </summary>
    public enum CodeEnum
    {
        Success = 200, //成功
        PartSuccess = 201, //部分成功
        Notfound = 404, //不存在
        BadRequest = 400,//由于语法错误，服务器无法理解该请求 
        IsExist = 405,//存在，不能重复
        IsDeleted = 406,//要删除的已经被删除
        UnKnown = 407,//未知的错误
        Statues = 408 //状态重复
    }
}
