using PLCCommunication_DomainService.IBaseService;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.IService
{
    public interface IUserService:IBaseService<User>
    {
        public Task<bool> DelByUserName(User user);
    }
}
