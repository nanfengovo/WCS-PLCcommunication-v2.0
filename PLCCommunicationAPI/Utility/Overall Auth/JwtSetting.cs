using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Utility.Overall_Auth
{
    public class JwtSetting
    {
        public string? SecKey { get; set; }

        public int ExpireSeconds { get; set; }

        public string? Issuer { get; set; }

        public string? Audience { get; set; }
    }
}
