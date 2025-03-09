using Microsoft.EntityFrameworkCore;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.DBContexts
{
    public class MyDbContext : DbContext
    {
        public DbSet<ModbusTCPConfig> modbusTCPConfigs { get; set; }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ModbusTCPConfig>()
        .HasNoKey(); // 指定这是一个无键实体

            //把当前程序集中实现了IEntityTypeConfiguration接口的类加载进来，配置sql
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        }
    }
}
