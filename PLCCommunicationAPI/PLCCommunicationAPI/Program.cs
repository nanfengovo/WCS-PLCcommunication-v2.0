
using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Utility.PlugInUnit;
using PLCCommunication_Utility.Plug_ins;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;
using PLCCommunication_Utility.Mapper;
using PLCCommunication_Model.Identity;
using Microsoft.AspNetCore.Identity;
using PLCCommunication_Utility.Overall_Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using PLCCommunication_Infrastructure.Migrations;
using PLCCommunication_API.PlugInUnit;
using NLog.Web;

namespace PLCCommunicationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加 CORS 策略服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DevCors", policy =>
                {
                    policy.WithOrigins("http://localhost:8889") // 前端地址
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // 如果前端需要传 cookies
                });
            });

            // 强制指定 URL 和端口
            builder.WebHost.UseUrls("http://localhost:8888", "https://localhost:8899");

            // Add services to the container.
            //自定义swagger中间件
            builder.Services.InitSwagger();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // 添加 NLog 服务
            // 清除默认的日志提供程序
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            //启用swagger鉴权组件
            builder.Services.AddSwaggerGen(opt =>
            {

                var scheme = new OpenApiSecurityScheme()
                {
                    Description = $"Authorization header \r\n Example:'Bearer xxxxxx'",
                    Reference = new OpenApiReference() { Type = ReferenceType.SecurityScheme, Id = "Authorization" },
                    Scheme = "oauth2",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                };

                opt.AddSecurityDefinition("Authorization",scheme);
                var requirement = new OpenApiSecurityRequirement();
                requirement[scheme] = new List<string>();
                opt.AddSecurityRequirement(requirement);
            });

            //自定义Mapper注入
            builder.Services.AddAutoMapper(typeof(DTOMapper));

            //读取配置文件中JWT的配置信息，然后通过Configuration配置系统注入到Controller层进行授权
            builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));

            //配置JWT 鉴权
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSetting>();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecKey);
                    var seckey = new SymmetricSecurityKey(keyBytes);

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,//代表分发web令牌的web 程序

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,//token的受理者

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = seckey,
                        ClockSkew = TimeSpan.FromSeconds(jwtSettings.ExpireSeconds)


                    };

                });



            // Add DbContext service
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //注入filter服务
            builder.Services.Configure<MvcOptions>(opt =>
            {
                opt.Filters.Add<JwtVersionCheckFilter>();
            });

            //自定义依赖注入
            builder.Services.AddCustomIOC();

            //Identity注入
            builder.Services.AddIdentityIOC();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
                //使用自定义swagger
                app.InitSwagger();
            }

            //添加到管道中，在app.UseAuthorization();前添加
            //鉴权
            app.UseAuthentication();

            // 应用 CORS 策略
            app.UseCors("DevCors");

            //授权
            app.UseAuthorization();

            app.MapControllers();

            app.Run();





        }
    
    }
    public static class IdentityIOC
    {
        /// <summary>
        /// 注入Identity
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityIOC(this IServiceCollection services)
        {
            //注入数据保护
            services.AddDataProtection();

            //配置IdentityCore
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true; //密码必须要有数字
                opt.Password.RequireNonAlphanumeric = false; //是否需要非数字非字母
                opt.Password.RequiredLength = 6; //长度最少6位
                opt.Password.RequireLowercase = false; //可以不包含小写字母
                opt.Password.RequireUppercase = false; //可以不包含大写字母


                opt.Lockout.MaxFailedAccessAttempts = 5; //最大登录失败次数
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //失败5次后冻结5min

            });


            //构建认证框架
            var idbuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            idbuilder.AddEntityFrameworkStores<MyDbContext>() //指定Identity用哪一个dbcontext做管理
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>();

            return services;
        }
    }

}
