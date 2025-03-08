using Microsoft.Extensions.Options;
using Read_Configuration02.Model;

namespace Read_Configuration02
{
    public class Demo
    {
        private readonly IOptionsSnapshot<DbSettings> optDbsettings;
        private readonly IOptionsSnapshot<SmtpSettings> optSmtpSettings;

        public Demo(IOptionsSnapshot<DbSettings> optDbsettings, IOptionsSnapshot<SmtpSettings> optSmtpSettings)
        {
            this.optDbsettings = optDbsettings;
            this.optSmtpSettings = optSmtpSettings;
        }


        public void Test()
        {
            var db = optDbsettings.Value;
            Console.WriteLine($"数据库：{db.DbType},{db.ConnectionString}");
            var smtp = optSmtpSettings.Value;
            Console.WriteLine($"smtp:{smtp.Server},{smtp.UserName},{smtp.Password}");
        }

    }
}
