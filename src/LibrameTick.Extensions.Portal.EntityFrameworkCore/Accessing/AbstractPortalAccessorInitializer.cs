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
/// 定义抽象实现 <see cref="IAccessorInitializer"/> 的内容访问器初始化器。
/// </summary>
/// <typeparam name="TAccessor">指定实现 <see cref="AbstractAccessor"/> 与 <see cref="IPortalAccessor{TUser}"/> 的访问器类型。</typeparam>
/// <typeparam name="TSeeder">指定实现 <see cref="AbstractPortalAccessorSeeder{TUser}"/> 的种子机类型。</typeparam>
/// <typeparam name="TUser">指定实现 <see cref="IUser"/> 的用户类型。</typeparam>
public abstract class AbstractPortalAccessorInitializer<TAccessor, TSeeder, TUser> : AbstractContentAccessorInitializer<TAccessor, TSeeder>
    where TAccessor : AbstractAccessor, IPortalAccessor<TUser>
    where TSeeder : AbstractPortalAccessorSeeder<TUser>
    where TUser : class, IUser
{
    /// <summary>
    /// 构造一个 <see cref="AbstractPortalAccessorInitializer{TAccessor, TSeeder, TUser}"/>。
    /// </summary>
    /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
    /// <param name="seeder">给定的 <typeparamref name="TSeeder"/>。</param>
    protected AbstractPortalAccessorInitializer(TAccessor accessor, TSeeder seeder)
        : base(accessor, seeder)
    {
    }


    /// <summary>
    /// 填充访问器。
    /// </summary>
    /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
    protected override void Populate(IServiceProvider services)
    {
        base.Populate(services);

        TryPopulateDbSet(Seeder.GetUsers, accssor => accssor.Users);

        TryPopulateDbSet(Seeder.GetEditors, accssor => accssor.Editors);
    }

    /// <summary>
    /// 异步填充访问器。
    /// </summary>
    /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
    /// <returns>返回一个异步操作。</returns>
    protected override async Task PopulateAsync(IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        await base.PopulateAsync(services, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetUsersAsync(token),
            accessor => accessor.Users, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetEditorsAsync(token),
            accessor => accessor.Editors, cancellationToken);
    }

}
