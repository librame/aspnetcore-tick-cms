#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Content.Accessing;

/// <summary>
/// 定义适用于 MySQL 的内容访问器。
/// </summary>
public class MySqlContentAccessor : AbstractContentAccessor<MySqlContentAccessor>
{
    /// <summary>
    /// 构造一个 <see cref="MySqlContentAccessor"/>。
    /// </summary>
    /// <param name="options">给定的 <see cref="DbContextOptions{MySqlContentAccessor}"/>。</param>
    public MySqlContentAccessor(DbContextOptions<MySqlContentAccessor> options)
        : base(options)
    {
    }

}
