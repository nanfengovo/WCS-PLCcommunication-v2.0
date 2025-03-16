using Microsoft.EntityFrameworkCore;
using PLCCommunication_Infrastructure.DBContexts;
using PLCCommunication_Infrastructure.IBaseRespository;
using PLCCommunication_Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.BaseRespository
{
    public class BaseRespository<TEntity> : IBaseRespository<TEntity>where TEntity : class ,new()
    {

        protected  MyDbContext _ctx;

        public async Task<bool> CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _ctx.Set<TEntity>().AddAsync(entity);
            return await _ctx.SaveChangesAsync()>0;
        }

        public virtual async Task<bool> DeletedAsync(TEntity entity)
        {
            return await UpdateAsync(entity);
        }

        public virtual async Task<List<TEntity>> FindAllAsync()
        {
            return await _ctx.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> del)
        {
           return await _ctx.Set<TEntity>().Where(del).ToListAsync();
        }

        public virtual async Task<TEntity> FindEntityByAsync(Expression<Func<TEntity, bool>> del)
        {
            return await _ctx.Set<TEntity>().FirstOrDefaultAsync(del);
        }

        public virtual async Task<TEntity> FindEntityByIdAsync(Guid id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> FindEntityByIdAsync(int  id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
            return await _ctx.SaveChangesAsync() > 0;
        }
    }
}
