using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.DTO
{
    public class S7ConfigDTO
    {
       

        /// <summary>
        /// 配置名
        /// </summary>
        public string ProxyName { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// DB块id
        /// </summary>
        public int DBID { get; set; }

        /// <summary>
        /// 地址偏移
        /// </summary>

        public string Address { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 位地址
        /// </summary>
        public int bit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsOpen { get; set; }
    }
}
