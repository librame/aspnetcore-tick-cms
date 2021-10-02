#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data.Accessing;

namespace Librame.Extensions.Content.Accessing;

/// <summary>
/// 定义抽象实现 <see cref="IAccessorInitializer"/> 的内容访问器初始化器。
/// </summary>
/// <typeparam name="TAccessor">指定实现 <see cref="AbstractAccessor"/> 与 <see cref="IContentAccessor"/> 的访问器类型。</typeparam>
/// <typeparam name="TSeeder">指定实现 <see cref="AbstractContentAccessorSeeder"/> 的种子机类型。</typeparam>
public abstract class AbstractContentAccessorInitializer<TAccessor, TSeeder> : AbstractAccessorInitializer<TAccessor, TSeeder>
    where TAccessor : AbstractAccessor, IContentAccessor
    where TSeeder : AbstractContentAccessorSeeder
{
    /// <summary>
    /// 构造一个 <see cref="AbstractContentAccessorInitializer{TAccessor, TSeeder}"/>。
    /// </summary>
    /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
    /// <param name="seeder">给定的 <typeparamref name="TSeeder"/>。</param>
    protected AbstractContentAccessorInitializer(TAccessor accessor, TSeeder seeder)
        : base(accessor, seeder)
    {
        Seeder = seeder;
    }


    /// <summary>
    /// 填充访问器。
    /// </summary>
    /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
    protected override void Populate(IServiceProvider services)
    {
        TryPopulateDbSet(Seeder.GetCategories, accssor => accssor.Categories);

        TryPopulateDbSet(Seeder.GetClaims, accssor => accssor.Claims);

        TryPopulateDbSet(Seeder.GetPanes, accssor => accssor.Panes);

        TryPopulateDbSet(Seeder.GetSources, accssor => accssor.Sources);

        TryPopulateDbSet(Seeder.GetTags, accssor => accssor.Tags);

        TryPopulateDbSet(Seeder.GetUnits, accssor => accssor.Units);
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
        await TryPopulateDbSetAsync(async token => await Seeder.GetCategoriesAsync(token),
            accessor => accessor.Categories, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetClaimsAsync(token),
            accessor => accessor.Claims, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetPanesAsync(token),
            accessor => accessor.Panes, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetSourcesAsync(token),
            accessor => accessor.Sources, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetTagsAsync(token),
            accessor => accessor.Tags, cancellationToken);

        await TryPopulateDbSetAsync(async token => await Seeder.GetUnitsAsync(token),
            accessor => accessor.Units, cancellationToken);
    }

}
