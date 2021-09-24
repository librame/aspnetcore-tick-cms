#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Data;

/// <summary>
/// <see cref="DbSet{TEntity}"/> 静态扩展。
/// </summary>
public static class ContentDbSetExtensions
{
    private static readonly object _locker = new object();


    /// <summary>
    /// 确定本地缓存或数据库序列中的任何元素是否满足条件。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <param name="dbSet">给定的 <see cref="DbSet{TEntity}"/>。</param>
    /// <param name="predicate">给定的断定方法。</param>
    /// <param name="checkLocal">是否检查本地缓存（可选；默认优先检查本地缓存）。</param>
    /// <returns>返回是否存在的布尔值。</returns>
    public static IEnumerable<TEntity> LocalOrDbWhere<TEntity>(this DbSet<TEntity> dbSet,
        Expression<Func<TEntity, bool>> predicate, bool checkLocal = true)
        where TEntity : class
    {
        lock (_locker)
        {
            if (checkLocal)
            {
                var local = dbSet.Local.Where(predicate.Compile());
                if (local.Any())
                    return local;
            }

            return dbSet.Where(predicate);
        }
    }

    /// <summary>
    /// 确定本地缓存或数据库序列中的任何元素是否满足条件。
    /// </summary>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <param name="dbSet">给定的 <see cref="DbSet{TEntity}"/>。</param>
    /// <param name="predicate">给定的断定方法。</param>
    /// <param name="checkLocal">是否检查本地缓存（可选；默认优先检查本地缓存）。</param>
    /// <returns>返回是否存在的布尔值。</returns>
    public static TEntity? LocalOrDbFirstOrDefault<TEntity>(this DbSet<TEntity> dbSet,
        Expression<Func<TEntity, bool>> predicate, bool checkLocal = true)
        where TEntity : class
    {
        lock (_locker)
        {
            if (checkLocal)
            {
                var local = dbSet.Local.FirstOrDefault(predicate.Compile());
                if (local is not null)
                    return local;
            }

            return dbSet.FirstOrDefault(predicate);
        }
    }

}
