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

namespace Librame.Extensions.Portal.Accessing;

/// <summary>
/// 定义抽象实现 <see cref="IPortalAccessor{TUser}"/> 的门户访问器。
/// </summary>
/// <typeparam name="TAccessor">指定的门户访问器类型。</typeparam>
/// <typeparam name="TUser">指定实现 <see cref="IUser"/> 的用户类型。</typeparam>
public abstract class AbstractPortalAccessor<TAccessor, TUser> : AbstractContentAccessor<TAccessor>, IPortalAccessor<TUser>
    where TAccessor : AbstractDataAccessor, IPortalAccessor<TUser>
    where TUser : class, IUser
{

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

    /// <summary>
    /// 构造一个 <see cref="AbstractPortalAccessor{TAccessor, TUser}"/>。
    /// </summary>
    /// <param name="options">给定的 <see cref="DbContextOptions{TAccessor}"/>。</param>
    protected AbstractPortalAccessor(DbContextOptions<TAccessor> options)
        : base(options)
    {
    }

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。


    /// <summary>
    /// 门户选项。
    /// </summary>
    public PortalExtensionOptions PortalOptions
        => this.GetService<PortalExtensionOptions>();


    /// <summary>
    /// 编者数据集。
    /// </summary>
    public DbSet<Editor> Editors { get; set; }

    /// <summary>
    /// 用户数据集。
    /// </summary>
    public DbSet<TUser> Users { get; set; }


    /// <summary>
    /// 开始数据模型创建。
    /// </summary>
    /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
    protected override void OnDataModelCreating(ModelBuilder modelBuilder)
    {
        base.OnDataModelCreating(modelBuilder);

        modelBuilder.CreatePortalModel(this);
    }

}
