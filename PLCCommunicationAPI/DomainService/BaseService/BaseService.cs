using PLCCommunication_DomainService.IBaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PLCCommunication_Infrastructure.IBaseRespository;
using System.Linq.Expressions;

namespace PLCCommunication_DomainService.BaseService
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {

        protected IBaseRespository<TEntity> _respository;

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await _respository.CreateAsync(entity);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletedAsync(TEntity entity)
        {
            return await _respository.DeletedAsync(entity);
        }

        public async Task<List<TEntity>> FindAllAsync()
        {
           return await _respository.FindAllAsync();
        }

        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> del)
        {
            return await _respository.FindAllAsync(del);
        }

        public async Task<TEntity> FindEntityByAsync(Expression<Func<TEntity, bool>> del)
        {
            return await _respository.FindEntityByAsync(del);
        }

        public async Task<TEntity> FindEntityByIdAsync(Guid  id)
        {
            return await _respository.FindEntityByIdAsync(id);
        }

        public async Task<TEntity> FindEntityByIdAsync(int id)
        {
            return await _respository.FindEntityByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _respository.UpdateAsync(entity);
        }
    }
}
