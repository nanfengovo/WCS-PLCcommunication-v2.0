using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using PLCCommunication_Model.Identity;
using System.Security.Claims;

namespace PLCCommunication_API.PlugInUnit
{
    public class JwtVersionCheckFilter : IAsyncActionFilter
    {

        private readonly UserManager<User> _userManager;

        public JwtVersionCheckFilter(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor == null)
            {
                //访问的就不是action方法
                await next();
                return;
            }

            //检查是否携带NotCheck特性
            if(descriptor.MethodInfo.GetCustomAttributes(typeof(NotCheckJwtVersionAttribute), true).Any())
            {
                await next();
                return;
            }


            var claimJwtVersion = context.HttpContext.User.FindFirst("JwtVersion");
            if (claimJwtVersion is null)
            {
                context.Result = new ObjectResult("没有找到JwtVersion的内容") { StatusCode = 400 };
                return;
            }

            var UserId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            var user = await _userManager.FindByIdAsync(UserId.Value);
            if (user is null)
            {
                context.Result = new ObjectResult("在数据库中找不到该用户") { StatusCode = 400 };
                return;
            }
            long JwtVersionFromClient = long.Parse(claimJwtVersion.Value);//转换为long类

            if (user.JwtVersion > JwtVersionFromClient)//如果当前用户的JwtVersion小于数据库中的JwtVersion,则代表用户已做出修改、或登出
            {
                context.Result = new ObjectResult("jwt过时") { StatusCode = 400 };
                return;
            }

            await next();

        }
    }
}