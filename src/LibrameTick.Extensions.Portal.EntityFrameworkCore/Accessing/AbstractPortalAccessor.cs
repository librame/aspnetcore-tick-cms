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
using Librame.Extensions.Data.Accessing;
using Librame.Extensions.Portal.Storing;
using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Portal.Accessing
{
    /// <summary>
    /// 定义抽象实现 <see cref="IPortalAccessor"/> 的门户访问器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的门户访问器类型。</typeparam>
    public abstract class AbstractPortalAccessor<TAccessor> : AbstractContentAccessor<TAccessor>, IPortalAccessor
        where TAccessor : AbstractAccessor, IPortalAccessor
    {

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// 构造一个 <see cref="AbstractPortalAccessor{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{TAccessor}"/>。</param>
        protected AbstractPortalAccessor(DbContextOptions<TAccessor> options)
            : base(options)
        {
        }


        /// <summary>
        /// 编者数据集。
        /// </summary>
        public DbSet<Editor> Editors { get; set; }

        /// <summary>
        /// 用户数据集。
        /// </summary>
        public DbSet<IntegrationUser> Users { get; set; }

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。


        /// <summary>
        /// 附加模型创建。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void AppendModelCreating(ModelBuilder modelBuilder)
        {
            //var options = this.GetService<PortalExtensionOptions>();
            modelBuilder.CreatePortalModel();
        }

    }
}
