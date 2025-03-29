using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PLCCommunication_Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Configs
{
    public class ModbusTCPbackgroundServiceConfig : IEntityTypeConfiguration<ModbusTCPBackgroundService>
    {
        public void Configure(EntityTypeBuilder<ModbusTCPBackgroundService> builder)
        {
            //配置表名
            builder.ToTable("ModbusTCPBackgroundService");

            //软删除实现
            builder.HasQueryFilter(x => x.IsDeleted== false);
        }
    }
}
