using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledTasks.ModbusTcp
{
    public interface IModbusTCP
    {
        Task<bool> InitializeAsync();
    }
}
