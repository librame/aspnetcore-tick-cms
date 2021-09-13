#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Portal.Storing;
using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Portal.Accessing
{
    /// <summary>
    /// 定义适用于 SQLServer 的门户访问器。
    /// </summary>
    /// <typeparam name="TUser">指定实现 <see cref="IUser"/> 的用户类型。</typeparam>
    public class SqlServerPortalAccessor<TUser> : AbstractPortalAccessor<SqlServerPortalAccessor<TUser>, TUser>
        where TUser : class, IUser
    {
        /// <summary>
        /// 构造一个 <see cref="SqlServerPortalAccessor{TUser}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{SqlServerPortalAccessor}"/>。</param>
        public SqlServerPortalAccessor(DbContextOptions<SqlServerPortalAccessor<TUser>> options)
            : base(options)
        {
        }

    }
}
