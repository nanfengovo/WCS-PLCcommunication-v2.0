using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PLCCommunication_Model.Entities
{
    public class SysMenu
    {
        /// <summary>
        /// 菜单唯一标识
        /// </summary>
        [Key]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        [Required]
        public Role Role { get; set; }

        /// <summary>
        /// 菜单显示名称
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 菜单类型 (1-父菜单，2-子菜单)
        /// </summary>
        [JsonPropertyName("type")]
        public int? Type { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [JsonPropertyName("url")]
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// 图标类名
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        [JsonPropertyName("sort")]
        public int Sort { get; set; }

        /// <summary>
        /// 子菜单列表（父菜单专用）
        /// </summary>
        [JsonPropertyName("children")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SysMenu>? Children { get; set; }

        /// <summary>
        /// 父菜单ID（子菜单专用）
        /// </summary>
        [JsonPropertyName("parentId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
