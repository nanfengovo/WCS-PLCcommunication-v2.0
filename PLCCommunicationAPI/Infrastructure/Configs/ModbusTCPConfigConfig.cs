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
    internal class ModbusTCPConfigConfig : IEntityTypeConfiguration<ModbusTCPConfig>
    {
        public void Configure(EntityTypeBuilder<ModbusTCPConfig> builder)
        {
            //配置表名
            builder.ToTable("T_ModbusTCPConfig");

            //软删除实现
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
