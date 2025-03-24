using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.MiniExcelModel
{
    public class S7Excel
    {
        /// <summary>
        /// 配置名
        /// </summary>
        [ExcelColumnName("配置名")]
        public string ProxyName { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        [ExcelColumnName("ip")]
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        [ExcelColumnName("端口")]
        public int Port { get; set; }

        /// <summary>
        /// DB块id
        /// </summary>
         [ExcelColumnName("DB Id")]
        public int DBID { get; set; }

        /// <summary>
        /// 地址偏移
        /// </summary>
        [ExcelColumnName("地址偏移")]
        public string Address { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [ExcelColumnName("数据类型")]
        public string Type { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        [ExcelColumnName("数据长度")]
        public int Length { get; set; }

        /// <summary>
        /// 位地址
        /// </summary>
        [ExcelColumnName("位地址")]
        public int bit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [ExcelColumnName("备注")]
        public string? Remark { get; set; }

        ///// <summary>
        ///// 是否启用
        ///// </summary>
        //public bool IsOpen { get; set; }

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
