using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace LandWind.Blog.Core.Domain
{
    /// <summary>
    ///默认接口，Key=int
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepositoryBase<TEntity> : IRepositoryBase<TEntity,int> where TEntity : class, IEntity<int>
    {
         
    }

    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IRepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        ///// <summary>
        ///// 获取对象Dto
        ///// </summary>
        ///// <typeparam name="TDto"></typeparam>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //Task<TDto> GetAsync<TDto>(TKey key);

        ///// <summary>
        ///// 根据条件获取对象实体Dto
        ///// </summary>
        ///// <typeparam name="TDto"></typeparam>
        ///// <param name="expression"></param>
        ///// <returns></returns>
        //Task<TDto> GetAsync<TDto>(Expression<Func<TEntity, bool>> expression);

        ///// <summary>
        ///// 根据条件获取实体
        ///// </summary>
        ///// <param name="exp"></param>
        ///// <returns></returns>
        //Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> exp); 
          
        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> DeleteAsync(TKey id);

        ///// <summary>
        ///// 批量删除
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //Task<bool> BatchDeleteAsync(TKey[] ids);
    }
}
