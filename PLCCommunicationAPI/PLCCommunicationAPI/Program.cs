
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

namespace PLCCommunicationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //�Զ���swagger�м��
            builder.Services.InitSwagger();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            //����swagger��Ȩ���
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

            //�Զ���Mapperע��
            builder.Services.AddAutoMapper(typeof(DTOMapper));

            //��ȡ�����ļ���JWT��������Ϣ��Ȼ��ͨ��Configuration����ϵͳע�뵽Controller�������Ȩ
            builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));

            //����JWT ��Ȩ
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSetting>();
                    byte[] keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SecKey);
                    var seckey = new SymmetricSecurityKey(keyBytes);

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,//����ַ�web���Ƶ�web ����

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,//token��������

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = seckey,
                        ClockSkew = TimeSpan.FromSeconds(jwtSettings.ExpireSeconds)


                    };

                })
                ;



            // Add DbContext service
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //�Զ�������ע��
            builder.Services.AddCustomIOC();

            //Identityע��
            builder.Services.AddIdentityIOC();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
                //ʹ���Զ���swagger
                app.InitSwagger();
            }

            //��ӵ��ܵ��У���app.UseAuthorization();ǰ���
            //��Ȩ
            app.UseAuthentication();


            //��Ȩ
            app.UseAuthorization();

            app.MapControllers();

            app.Run();





        }
    
    }
    public static class IdentityIOC
    {
        /// <summary>
        /// ע��Identity
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityIOC(this IServiceCollection services)
        {
            //ע�����ݱ���
            services.AddDataProtection();

            //����IdentityCore
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = true; //�������Ҫ������
                opt.Password.RequireNonAlphanumeric = false; //�Ƿ���Ҫ�����ַ���ĸ
                opt.Password.RequiredLength = 6; //��������6λ
                opt.Password.RequireLowercase = false; //���Բ�����Сд��ĸ
                opt.Password.RequireUppercase = false; //���Բ�������д��ĸ


                opt.Lockout.MaxFailedAccessAttempts = 5; //����¼ʧ�ܴ���
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); //ʧ��5�κ󶳽�5min

            });


            //������֤���
            var idbuilder = new IdentityBuilder(typeof(User), typeof(Role), services);
            idbuilder.AddEntityFrameworkStores<MyDbContext>() //ָ��Identity����һ��dbcontext������
                .AddDefaultTokenProviders()
                .AddUserManager<UserManager<User>>()
                .AddRoleManager<RoleManager<Role>>();

            return services;
        }
    }

}
