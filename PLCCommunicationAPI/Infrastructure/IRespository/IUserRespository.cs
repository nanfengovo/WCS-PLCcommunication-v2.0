﻿using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IRespository
{
    public interface IUserRespository:IBaseRespository<User>
    {
        public Task<bool> DelByUserName(User user);
    }
}
