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
    /// 定义适用于 SQLServer 的门户访问器。
    /// </summary>
    public class SqlServerPortalAccessor : AbstractPortalAccessor<SqlServerPortalAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="SqlServerPortalAccessor"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{SqlServerPortalAccessor}"/>。</param>
        public SqlServerPortalAccessor(DbContextOptions<SqlServerPortalAccessor> options)
            : base(options)
        {
        }

    }
}
