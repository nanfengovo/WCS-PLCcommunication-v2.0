
using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Utility.PlugInUnit;
using PLCCommunication_Utility.Plug_ins;
using PLCCommunication_DomainService.IService;
using PLCCommunication_DomainService.Service;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Respository;

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
            builder.Services.AddSwaggerGen();

            // Add DbContext service
            builder.Services.AddDbContext<MyDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //�Զ�������ע��
            builder.Services.AddCustomIOC();

            //ֻע������ͬʱ��Ҫע��ִ�������������ڲִ���
            //builder.Services.AddScoped<ModbusTCPConfigService>();
            //builder.Services.AddScoped<IModbusTCPConfigService>();

            //builder.Services.AddScoped<ModbusTCPConfigResposity>();
            //builder.Services.AddScoped<IModbusTCPConfigResposity>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
                //ʹ���Զ���swagger
                app.InitSwagger();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();





        }

    }
}
