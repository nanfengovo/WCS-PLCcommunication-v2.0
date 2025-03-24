using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.MiniExcelHelper
{
    public class ImportResult
    {
        public int Code { get; set; }

        public string? msg { get; set; }
        public int TotalCount { get; set; }
        public int SuccessCount { get; set; }
        public List<string> ErrorMessages { get; set; } = new();
    }
}
