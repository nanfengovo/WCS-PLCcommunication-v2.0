using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PLCCommunication_Utility.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.PlugInUnit
{
    /// <summary>
    /// swagger插件
    /// </summary>
    public static class SwaggerPlugInUnit
    {
        /// <summary>
        /// 初始化Swagger
        /// </summary>
        /// <param name="services"></param>
        public static void InitSwagger(this IServiceCollection services)
        {
            //添加swagger
            services.AddSwaggerGen(options =>
            {
                typeof(ModeuleGroupEnum).GetEnumNames().ToList().ForEach(version =>
                {
                    options.SwaggerDoc(version, new OpenApiInfo()
                    {
                        Title = "WCS管理系统",
                        Version = "V2.0",
                        Description = "Vue3+WebAPI",
                        Contact = new OpenApiContact { Name = "nanfengovo", Url = new Uri("https://github.com/nanfengovo") }
                    });

                });

                //反射获取接口及方法描述
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName), true);

            });
        }

        /// <summary>
        /// swagger加入路由和管道
        /// </summary>
        /// <param name="app"></param>
        public static void InitSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                typeof(ModeuleGroupEnum).GetEnumNames().ToList().ForEach(version =>
                {
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"接口分类{version}");
                });
            });
        }
    }
}
