using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PLCCommunication_Infrastructure.IBaseRespository
{
    public interface IBaseRespository<TEntity> where TEntity : class,new()
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> CreateAsync(TEntity entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> DeletedAsync(TEntity entity);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<bool> UpdateAsync(TEntity entity);
        
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public Task<List<TEntity>> FindAllAsync();

        /// <summary>
        /// 根据特定条件查询所有
        /// </summary>
        /// <param name="del"></param>
        /// <returns></returns>
        public Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> del);

        /// <summary>
        /// 根据ID查
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<TEntity> FindEntityByIdAsync(Guid id);

        public Task<TEntity> FindEntityByIdAsync(int id);

        /// <summary>
        /// 根据特定的条件查询所有
        /// </summary>
        /// <param name="del"></param>
        /// <returns></returns>
        public Task<TEntity> FindEntityByAsync(Expression<Func<TEntity, bool>> del);



    }
}
