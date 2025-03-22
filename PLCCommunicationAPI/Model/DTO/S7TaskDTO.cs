using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.DTO
{
    public class S7TaskDTO
    {
        /// <summary>
        /// 任务名
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 读取的DB块地址
        /// </summary>
        [Required]
        public string DBaddr { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsOpen { get; set; }

       
    }
}
