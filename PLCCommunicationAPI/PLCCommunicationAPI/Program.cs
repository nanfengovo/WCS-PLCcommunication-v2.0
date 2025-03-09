
using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Utility.PlugInUnit;

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
