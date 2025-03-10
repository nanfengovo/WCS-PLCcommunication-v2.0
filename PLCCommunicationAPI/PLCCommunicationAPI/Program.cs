
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

namespace PLCCommunicationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //自定义swagger中间件
            builder.Services.InitSwagger();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //自定义Mapper注入
            builder.Services.AddAutoMapper(typeof(DTOMapper));

            // Add DbContext service
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
