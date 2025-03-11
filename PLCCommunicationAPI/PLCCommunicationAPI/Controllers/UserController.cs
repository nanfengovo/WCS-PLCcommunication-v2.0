using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Model.Identity;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;
using PLCCommunication_Utility.Request;

namespace PLCCommunication_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.User))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        /// <summary>
        /// 测试获取环境变量
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="mapper"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public UserController(IUserService userService, IMapper mapper, UserManager<User> userManager,RoleManager<Role> roleManager)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 测试Identity是否引入
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Result> CreateUser(CheckRequestInfo info)
        {
            User user = new User
            {
                UserName = info.userName,
                IsDeleted = false
            };

            var b = await _userManager.CreateAsync(user, info.userPwd);
            if (!b.Succeeded)
            {
                // 获取并返回错误信息
                var errors = b.Errors.Select(e => e.Description).ToList();
                return new Result { Code = 401, Msg = "创建用户失败！！", Data = errors };
            }

            //添加角色
            if (await _userManager.IsInRoleAsync(user, "Normal"))
            {
                await _userManager.AddToRoleAsync(user, "Normal");
            }

            return new Result { Code = 200, Msg = "创建成功！" };


            //var data =await _roleManager.CreateAsync(new Role() {Name = "Normal" });
            //return new Result { Code = 200, Data = data.Succeeded };

        }
    }

    
}
