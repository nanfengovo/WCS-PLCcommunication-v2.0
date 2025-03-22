using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.Entities
{
    public class S7ReadTask
    {
        /// <summary>
        /// id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        /// 读取的结果
        /// </summary>
        public string? Result { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; } = false;


        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Createtime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModified { get; set; }
    }
}
