using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Model.Identity
{
    public class Role:IdentityRole<Guid>
    {
        public bool IsDeleted { get; set; }     
    }
}
