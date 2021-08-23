#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Portal.Accessing
{
    /// <summary>
    /// 定义适用于 MySQL 的门户访问器。
    /// </summary>
    public class MySqlPortalAccessor : AbstractPortalAccessor<MySqlPortalAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="MySqlPortalAccessor"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{MySqlPortalAccessor}"/>。</param>
        public MySqlPortalAccessor(DbContextOptions<MySqlPortalAccessor> options)
            : base(options)
        {
        }

    }
}
