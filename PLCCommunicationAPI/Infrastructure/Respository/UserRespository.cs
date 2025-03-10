using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class UserRespository:BaseRespository<User>,IUserRespository
    {
        private readonly MyDbContext _dbContext;

        public UserRespository(MyDbContext dbContext)
        {
            base._ctx = dbContext;
            _dbContext = dbContext;
        }
    }
}
