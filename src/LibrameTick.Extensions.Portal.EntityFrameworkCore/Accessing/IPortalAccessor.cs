#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Accessing;
using Librame.Extensions.Portal.Storing;
using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Portal.Accessing
{
    /// <summary>
    /// 定义实现 <see cref="IContentAccessor"/> 的门户访问器接口。
    /// </summary>
    public interface IPortalAccessor : IContentAccessor
    {
        /// <summary>
        /// 编者数据集。
        /// </summary>
        DbSet<Editor> Editors { get; set; }

        /// <summary>
        /// 用户数据集。
        /// </summary>
        DbSet<IntegrationUser> Users { get; set; }
    }
}
