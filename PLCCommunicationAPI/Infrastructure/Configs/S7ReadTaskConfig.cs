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
    public class S7ReadTaskConfig : IEntityTypeConfiguration<S7ReadTask>
    {
        public void Configure(EntityTypeBuilder<S7ReadTask> builder)
        {
            //配置表名
            builder.ToTable("S7ReadTask");

            //软删除实现
            builder.HasQueryFilter(x => x.IsDeleted == false);
        }
    }
}
