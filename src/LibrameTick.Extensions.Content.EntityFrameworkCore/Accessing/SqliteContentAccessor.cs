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
    /// 定义适用于 SQLite 的内容访问器。
    /// </summary>
    public class SqliteContentAccessor : AbstractContentAccessor<SqliteContentAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="SqliteContentAccessor"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{SqliteContentAccessor}"/>。</param>
        public SqliteContentAccessor(DbContextOptions<SqliteContentAccessor> options)
            : base(options)
        {
        }

    }
}
