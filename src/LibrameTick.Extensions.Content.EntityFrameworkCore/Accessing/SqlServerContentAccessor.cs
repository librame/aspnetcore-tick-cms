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

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// 定义适用于 SQLServer 的内容访问器。
    /// </summary>
    public class SqlServerContentAccessor : AbstractContentAccessor<SqlServerContentAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="SqlServerContentAccessor"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{SqlServerContentAccessor}"/>。</param>
        public SqlServerContentAccessor(DbContextOptions<SqlServerContentAccessor> options)
            : base(options)
        {
        }

    }
}
