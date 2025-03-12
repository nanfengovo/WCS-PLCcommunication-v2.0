using PLCCommunication_DomainService.BaseService;
using PLCCommunication_DomainService.IService;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_DomainService.Service
{
    public class UserService:BaseService<User>,IUserService
    {
        private readonly IUserRespository _userrespository;

        public UserService(IUserRespository userrespository)
        {
            base._respository = userrespository;
            _userrespository = userrespository;
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<bool> DelByUserName(User user)
        {
            return await _userrespository.DelByUserName(user);
        }

        
       
    }
}
