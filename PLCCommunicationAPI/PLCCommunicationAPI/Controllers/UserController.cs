using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Model.APIModel;
using PLCCommunication_Model.DTO;
using PLCCommunication_Model.Identity;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;
using PLCCommunication_Utility.Request;

namespace PLCCommunication_API.Controllers
{
    /// <summary>
    /// 用户管理的模块
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.User))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, UserManager<User> userManager, RoleManager<Role> roleManager, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取所有的用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            _logger.LogInformation("获取所有的用户-获取所有用户");
            return Ok(users);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            // 手动构建 User 对象
            var user = new User
            {
                UserName = request.UserName
            };
            //新加的用户是否存在
            var isExist = await _userManager.FindByNameAsync(request.UserName);
            if (isExist != null)
            {
                _logger.LogWarning("添加用户-添加用户失败！想要添加的用户已存在");
                return BadRequest("添加用户失败！想要添加的用户已存在");
            }
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                _logger.LogWarning($"添加用户-添加用户失败{result.Errors}");
                return BadRequest($"添加用户失败！{result.Errors}");
            }
            _logger.LogInformation($"成功添加用户名为{user.UserName}的用户");
            return Ok();
        }


        /// <summary>
        /// 给用户分配角色
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserToRole([FromBody] UserToRole request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var isExist =await  _userManager.FindByNameAsync(request.UserName);
            if(isExist == null)
            {
                _logger.LogWarning("给用户分配角色-需要分配角色的用户不存在！");
                return BadRequest("需要分配角色的用户不存在！");
            }
            var isRoleExist = await _roleManager.FindByNameAsync(request.RoleName);
            if (isRoleExist == null)
            {
                _logger.LogWarning($"给用户分配角色- {request.RoleName}角色不存在！");
                return BadRequest($"Role {request.RoleName}角色不存在!请先新建角色");
            }
            var isinRole = await _userManager.IsInRoleAsync(user,request.RoleName); 
            if(!isinRole)
            {
                var result = await _userManager.AddToRoleAsync(user,request.RoleName);
                if(!result.Succeeded)
                {
                    _logger.LogWarning($"给用户分配角色-未能将用户{request.UserName}分配{request.RoleName}角色！");
                }
            }
            _logger.LogInformation($"给用户分配角色-成功将用户{request.UserName}分配{request.RoleName}角色！");
            return Ok();
        }

        /// <summary>
        /// 根据用户名删除用户
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public async Task<Result> DelUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                _logger.LogWarning($"根据用户名删除用户-要删除的用户名{userName}不存在");
                return new Result { Code = 404, Msg = $"根据用户名删除用户-要删除的用户名{userName}不存在" };
            }
            var result = await _userService.DelByUserName(user);
            if(!result)
            {
                _logger.LogWarning($"根据用户名删除用户-删除用户名为{userName}的用户失败,该用户已经被删除！ ");
                return new Result { Code = 401, Msg = $"删除失败！删除用户名为{userName}的用户失败,该用户已经被删除！" };
            }
            _logger.LogInformation($"根据用户名删除用户-删除用户名为{userName}的用户成功！");
            return new Result { Code = 200 ,Msg = "删除成功" };


        }

    }


}
