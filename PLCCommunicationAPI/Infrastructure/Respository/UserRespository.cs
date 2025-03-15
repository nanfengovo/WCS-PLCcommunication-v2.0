using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.BaseRespository;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IRespository;
using PLCCommunication_Infrastructure.Migrations;
using PLCCommunication_Model.Entities;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.Respository
{
    public class UserRespository : BaseRespository<User>, IUserRespository
    {
        private readonly MyDbContext _dbContext;

        public UserRespository(MyDbContext dbContext)
        {
            base._ctx = dbContext;
            _dbContext = dbContext;
        }

        public override Task<bool> DeletedAsync(User entity)
        {
            return base.DeletedAsync(entity);
        }

        public override async Task<List<User>> FindAllAsync()
        {
            return await _dbContext.Users.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public override Task<List<User>> FindAllAsync(Expression<Func<User, bool>> del)
        {
            return base.FindAllAsync(del);
        }

        public override Task<User> FindEntityByAsync(Expression<Func<User, bool>> del)
        {
            return base.FindEntityByAsync(del);
        }

        public override Task<User> FindEntityByIdAsync(Guid id)
        {
            return base.FindEntityByIdAsync(id);
        }

        public override async Task<bool> UpdateAsync(User entity)
        {

           return await base.UpdateAsync(entity);
            
        }

        public async Task<bool> DelByUserName(User user)
        {
            if(user.IsDeleted)
                return false;
            user.IsDeleted = true;
            return await _ctx.SaveChangesAsync() > 0;
        }
    }


}
