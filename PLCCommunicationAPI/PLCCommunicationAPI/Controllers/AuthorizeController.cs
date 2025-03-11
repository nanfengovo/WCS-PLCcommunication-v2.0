using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PLCCommunication_API.PlugInUnit;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Model.Identity;
using PLCCommunication_Utility.APIHelper;
using PLCCommunication_Utility.Enum;
using PLCCommunication_Utility.Overall_Auth;
using PLCCommunication_Utility.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PLCCommunication_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = nameof(ModeuleGroupEnum.Authorize))]
    public class AuthorizeController : ControllerBase
    {
        private readonly IOptionsSnapshot<JwtSetting> _settings;

        private readonly IUserService _userService;

        private readonly UserManager<User> _userManager;

        public AuthorizeController(IOptionsSnapshot<JwtSetting> settings, IUserService userService, UserManager<User> userManager)
        {
            _settings = settings;
            _userService = userService;
            _userManager = userManager;
        }


        /// <summary>
        /// 登录鉴权
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [NotCheckJwtVersion]
        public async Task<Result> Login(CheckRequestInfo info)
        {
            var isExist = await _userService.FindEntityByAsync(x => x.UserName == info.userName);
            if (isExist == null)
            {
                return new Result { Code = 404, Msg = "用户名或密码错误" };
            }

            //判断是否被冻结
            if(await _userManager.IsLockedOutAsync(isExist))
            {
                return new Result { Code = 401, Msg = $"用户{isExist.UserName}已被冻结，请在{isExist.LockoutEnd}分后重试" };
            }

            //执行登录操作
            var result = await _userManager.CheckPasswordAsync(isExist,info.userPwd);
            if(result)//登录成功
            {
                //重置登录次数
                await _userManager.ResetAccessFailedCountAsync(isExist);

                isExist.JwtVersion++;
                await _userManager.UpdateAsync(isExist);

                //颁发令牌
                //1、声明payload
                //用户的Id和用户名
                List<Claim> claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier,isExist.Id.ToString()),
                new Claim(ClaimTypes.Name,isExist.UserName),
                new Claim("JwtVersion",isExist.JwtVersion.ToString())
                };

                //根据用户获取角色
                var roles = await _userManager.GetRolesAsync(isExist);
                foreach(var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }


                //2、生成JWT
                string? key = _settings.Value.SecKey;
                DateTime expire = DateTime.Now.AddSeconds(_settings.Value.ExpireSeconds);

                byte[] sceBytes = Encoding.UTF8.GetBytes(key);
                var seckey = new SymmetricSecurityKey(sceBytes);
                var credentials = new SigningCredentials(seckey, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new JwtSecurityToken(claims:claims,issuer:_settings.Value.Issuer,audience:_settings.Value.Audience,expires:expire,signingCredentials:credentials);


                string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
                //3、返回JWT
                return new Result { Code = 200, Data = jwt };

            }
            else
            {
                //记录登录失败的次数
                await _userManager.AccessFailedAsync(isExist);
                return new Result { Code = 404, Msg = "用户名或密码错误" };
            }
        }

    }
}
