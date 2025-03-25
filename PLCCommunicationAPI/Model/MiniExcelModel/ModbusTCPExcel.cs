using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.MiniExcelModel
{
    public class ModbusTCPExcel
    {

        /// <summary>
        /// 名称
        /// </summary>
        [ExcelColumnName("配置名")]
        public string? ProxyName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [ExcelColumnName("ip")]
        public string IP { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        [ExcelColumnName("端口")]
        public int Port { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [ExcelColumnName("设备id")]
        public byte SlaveID { get; set; }

        /// <summary>
        /// 功能码
        /// </summary>
        [ExcelColumnName("功能码")]
        public byte FunctionCode { get; set; }


        /// <summary>
        /// 变量地址
        /// </summary>
        [ExcelColumnName("变量地址")]
        public ushort StartAddress { get; set; }

        /// <summary>
        /// 个数
        /// </summary>
        [ExcelColumnName("数量")]
        public ushort Num { get; set; }

        ///// <summary>
        ///// 是否启用
        ///// </summary>
        //public bool IsOpen { get; set; }

        ///// <summary>
        ///// 软删除
        ///// </summary>
        //public bool IsDeleted { get; set; }

        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //public DateTime? Createtime { get; set; }

        ///// <summary>
        ///// 最后修改时间
        ///// </summary>
        //public DateTime? LastModified { get; set; }
    }
}
