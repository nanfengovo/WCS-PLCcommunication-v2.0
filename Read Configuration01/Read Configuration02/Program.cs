
using Read_Configuration02.Model;

namespace Read_Configuration02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // 添加服务到容器
            builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

            //builder.Services.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //var configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            //configurationBuilder.AddConfiguration();
            //builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smpt"));
            //IConfigurationRoot config = configurationBuilder.Build();
            //ServiceCollection services = new ServiceCollection();
            //services.AddOptions().Configure<DbSettings>(e => config.GetSection("DB").Bind(e))
            //    .Configure<SmtpSettings>(e => config.GetSection("Smtp").Bind(e));
            //services.AddTransient<Demo>();
            //using (var sp = services.BuildServiceProvider())
            //{
            //    //while (true)
            //    //{
            //        using (var scope = sp.CreateScope())
            //        {
            //            var spScope = scope.ServiceProvider;
            //            var demo = spScope.GetRequiredService<Demo>();
            //            demo.Test();
            //        }
            //        Console.WriteLine("可以修改配置了");
            //        Console.ReadKey();
            //    //}
            //}

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
