using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.Entities
{
    public class S7ReadWriteRecord
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// 配置名
        /// </summary>
        [Required]
        public string ProxyName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
         [Required]
        public string result { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string? ExceptionInfo { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
