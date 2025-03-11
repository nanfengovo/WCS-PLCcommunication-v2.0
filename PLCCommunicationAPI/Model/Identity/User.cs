using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.Identity
{
    public class User:IdentityUser<Guid>
    {
        public long JwtVersion { get; set; }
        public bool IsDeleted { get; set; }
    }
}
