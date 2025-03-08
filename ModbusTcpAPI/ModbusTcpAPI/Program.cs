
using ModbusTcpAPI.Controllers;
using ModbusTcpAPI.domainServer;

namespace ModbusTcpAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 添加服务到容器
            builder.Services.Configure<ModbusTCPConfig>(builder.Configuration.GetSection("ModbusTCPConfig"));
            builder.Services.AddScoped<ModbusTCPServer>();

            // 注册HttpClientFactory
            builder.Services.AddHttpClient();

            // 注册后台服务
            builder.Services.AddHostedService<ScheduledTaskService>();
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
