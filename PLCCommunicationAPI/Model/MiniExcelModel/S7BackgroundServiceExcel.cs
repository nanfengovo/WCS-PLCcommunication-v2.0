using MiniExcelLibs.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.MiniExcelModel
{
    public class S7BackgroundServiceExcel
    {
        /// <summary>
        /// 任务名
        /// </summary>
        [ExcelColumnName("定时任务名")]
        [Required]
        public string Name { get; set; }

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
        /// 读取的DB块地址
        /// </summary>
        [ExcelColumnName("变量地址")]
        [Required]
        public string DBaddr { get; set; }
    }
}
