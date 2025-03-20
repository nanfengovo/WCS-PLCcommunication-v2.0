using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.DBContexts
{
    public class MyDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<ModbusTCPConfig> modbusTCPConfigs { get; set; }
        public DbSet<S7Config> s7Configs { get; set; }

        public DbSet<S7ReadWriteRecord> s7ReadWriteRecords { get; set; }

        public DbSet<S7ReadTask> s7ReadTasks { get; set; }

        public MyDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ModbusTCPConfig>()
        .HasKey(m => m.Id); // 指定Id作为主键

            modelBuilder.Entity<S7Config>()
        .HasKey(m => m.Id); // 指定Id作为主键

            modelBuilder.Entity<S7ReadWriteRecord>()
        .HasKey(m => m.Id);

            modelBuilder.Entity<S7ReadTask>()
        .HasKey(m => m.Id);

            //把当前程序集中实现了IEntityTypeConfiguration接口的类加载进来，配置sql
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        }
    }
}
