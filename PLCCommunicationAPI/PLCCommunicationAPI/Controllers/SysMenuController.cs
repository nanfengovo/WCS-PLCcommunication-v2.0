using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_Model.Entities;
using PLCCommunication_Utility.Enum;

namespace PLCCommunication_API.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.SysMenu))]
    public class SysMenuController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IActionResult GetAllMenu()
        {
            var menuList = new List<MenuItem>
            {
                new MenuItem
                {
                    Id = 1,
                    Name = "系统管理",
                    Type = 1,
                    Url = "/main/system",
                    Icon = "el-icon-monitor",
                    Sort = 1,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id = 2,
                            Url = "/main/system/screen",
                            Name = "大屏数据展示",
                            Icon = "el-icon-Platform",
                            Sort = 1,
                            Type = 2,
                            Children = null,
                            ParentId = 1
                        },
                        new MenuItem
                        {
                            Id = 3,
                            Url = "/main/system/dashboard",
                            Name = "系统监控面板",
                            Icon = "el-icon-Histogram",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 1
                        },
                        new MenuItem
                        {
                            Id = 4,
                            Url = "/main/system/autoTask",
                            Name = "自动任务管理",
                            Icon = "el-icon-Open",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 1
                        }
                    }
                },
                new MenuItem
                {
                    Id = 5,
                    Name = "库位管理",
                    Type = 1,
                    Url = "/main/mapLocation",
                    Icon = "el-icon-MapLocation",
                    Sort = 1,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id = 6,
                            Url = "/main/mapLocation/location",
                            Name = "平面库位展示",
                            Icon = "el-icon-Location",
                            Sort = 1,
                            Type = 2,
                            Children = null,
                            ParentId = 5
                        },
                        new MenuItem
                        {
                            Id = 7,
                            Url = "/main/mapLocation/3dLocation",
                            Name = "3D库位展示",
                            Icon = "el-icon-AddLocation",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 5
                        }
                    }
                },
                new MenuItem
                {
                    Id = 8,
                    Name = "任务管理",
                    Type = 1,
                    Url = "/main/task",
                    Icon = "el-icon-Odometer",
                    Sort = 1,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id = 9,
                            Url = "/main/task/lift",
                            Name = "提升机任务管理",
                            Icon = "el-icon-Clock",
                            Sort = 1,
                            Type = 2,
                            Children = null,
                            ParentId = 8
                        },
                        new MenuItem
                        {
                            Id = 10,
                            Url = "/main/task/rgv",
                            Name = "四向车任务管理",
                            Icon = "el-icon-PieChart",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 8
                        }
                    }
                },
                new MenuItem
                {
                    Id = 11,
                    Name = "系统运维管理",
                    Type = 1,
                    Url = "/main/om",
                    Icon = "el-icon-Setting",
                    Sort = 1,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id = 12,
                            Url = "/main/om/modbus",
                            Name = "Modbus数据点运维",
                            Icon = "el-icon-Link",
                            Sort = 1,
                            Type = 2,
                            Children = null,
                            ParentId = 11
                        },
                        new MenuItem
                        {
                            Id = 13,
                            Url = "/main/om/S7",
                            Name = "S7数据点运维",
                            Icon = "el-icon-Cpu",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 11
                        }
                    }
                },
                new MenuItem
                {
                    Id = 14,
                    Name = "系统日志管理",
                    Type = 1,
                    Url = "/main/log",
                    Icon = "el-icon-TrendCharts",
                    Sort = 1,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id = 15,
                            Url = "/main/log/actionLog",
                            Name = "操作日志",
                            Icon = "el-icon-Document",
                            Sort = 1,
                            Type = 2,
                            Children = null,
                            ParentId = 14
                        },
                        new MenuItem
                        {
                            Id = 16,
                            Url = "/main/log/autoTaskLog",
                            Name = "自动任务日志",
                            Icon = "el-icon-ChromeFilled",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 14
                        },
                        new MenuItem
                        {
                            Id = 17,
                            Url = "/main/log/dbPointLog",
                            Name = "数据点读写日志",
                            Icon = "el-icon-Aim",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 14
                        }
                    }
                },
                new MenuItem
                {
                    Id = 18,
                    Name = "权限管理",
                    Type = 1,
                    Url = "/main/Permissions",
                    Icon = "el-icon-Setting",
                    Sort = 1,
                    Children = new List<MenuItem>
                    {
                        new MenuItem
                        {
                            Id = 19,
                            Url = "/main/Permissions/user",
                            Name = "用户",
                            Icon = "el-icon-UserFilled",
                            Sort = 1,
                            Type = 2,
                            Children = null,
                            ParentId = 18
                        },
                        new MenuItem
                        {
                            Id = 20,
                            Url = "/main/Permissions/role",
                            Name = "角色",
                            Icon = "el-icon-Avatar",
                            Sort = 2,
                            Type = 2,
                            Children = null,
                            ParentId = 18
                        }
                    }
                }
            };

            return Ok(menuList);
        }
    }
}
