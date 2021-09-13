#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// 定义抽象实现 <see cref="IAccessorInitializer"/>。
    /// </summary>
    /// <typeparam name="TAccessor">指定实现 <see cref="AbstractAccessor"/> 与 <see cref="IContentAccessor"/> 的访问器类型。</typeparam>
    /// <typeparam name="TSeeder">指定实现 <see cref="AbstractContentAccessorSeeder"/> 的种子机类型。</typeparam>
    public abstract class AbstractContentAccessorInitializer<TAccessor, TSeeder> : AbstractAccessorInitializer<TAccessor>
        where TAccessor : AbstractAccessor, IContentAccessor
        where TSeeder : AbstractContentAccessorSeeder
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractContentAccessorInitializer{TAccessor, TSeeder}"/>。
        /// </summary>
        /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
        /// <param name="seeder">给定的 <typeparamref name="TSeeder"/>。</param>
        protected AbstractContentAccessorInitializer(TAccessor accessor, TSeeder seeder)
            : base(accessor)
        {
            Seeder = seeder;
        }


        /// <summary>
        /// 访问器种子机。
        /// </summary>
        protected TSeeder Seeder { get; init; }

        /// <summary>
        /// 是否已填充。
        /// </summary>
        public bool IsPopulated { get; private set; }


        /// <summary>
        /// 通知填充。
        /// </summary>
        /// <returns>返回布尔值。</returns>
        protected virtual bool SetPopulating()
        {
            if (!IsPopulated)
                IsPopulated = true;

            return IsPopulated;
        }


        /// <summary>
        /// 填充访问器。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="options">给定的 <see cref="DataExtensionOptions"/>。</param>
        protected override void Populate(IServiceProvider services, DataExtensionOptions options)
        {
            if (!Accessor.Categories.LocalOrDbAny())
            {
                var categories = Seeder.GetCategories();

                Accessor.Categories.AddRange(categories);

                SetPopulating();
            }

            if (!Accessor.Claims.LocalOrDbAny())
            {
                var claims = Seeder.GetClaims();

                Accessor.Claims.AddRange(claims);

                SetPopulating();
            }

            if (!Accessor.Panes.LocalOrDbAny())
            {
                var panes = Seeder.GetPanes();

                Accessor.Panes.AddRange(panes);

                SetPopulating();
            }

            if (!Accessor.Sources.LocalOrDbAny())
            {
                var sources = Seeder.GetSources();

                Accessor.Sources.AddRange(sources);

                SetPopulating();
            }

            if (!Accessor.Tags.LocalOrDbAny())
            {
                var tags = Seeder.GetTags();

                Accessor.Tags.AddRange(tags);

                SetPopulating();
            }

            if (!Accessor.Units.LocalOrDbAny())
            {
                var units = Seeder.GetUnits();
                var paneIds = Seeder.GetPanes().Select(s => s.Id);
                var tagNames = Seeder.GetTags().Select(s => s.Name);
                
                Accessor.AddUnits(units, Seeder.IdGeneratorFactory, Seeder.Clock,
                    Seeder.GetInitialUserId()!, paneIds, claims: null, tagNames);

                SetPopulating();
            }

            if (IsPopulated)
            {
                Accessor.SaveChanges();
            }
        }

        /// <summary>
        /// 异步填充访问器。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="options">给定的 <see cref="DataExtensionOptions"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个异步操作。</returns>
        protected override async Task PopulateAsync(IServiceProvider services, DataExtensionOptions options,
            CancellationToken cancellationToken = default)
        {
            if (!await Accessor.Categories.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var categories = await Seeder.GetCategoriesAsync(cancellationToken);

                await Accessor.Categories.AddRangeAsync(categories, cancellationToken);

                SetPopulating();
            }

            if (!await Accessor.Claims.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var claims = await Seeder.GetClaimsAsync(cancellationToken);

                await Accessor.Claims.AddRangeAsync(claims, cancellationToken);

                SetPopulating();
            }

            if (!await Accessor.Panes.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var panes = await Seeder.GetPanesAsync(cancellationToken);

                await Accessor.Panes.AddRangeAsync(panes, cancellationToken);

                SetPopulating();
            }

            if (!await Accessor.Sources.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var sources = await Seeder.GetSourcesAsync(cancellationToken);

                await Accessor.Sources.AddRangeAsync(sources, cancellationToken);

                SetPopulating();
            }

            if (!await Accessor.Tags.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var tags = await Seeder.GetTagsAsync(cancellationToken);

                await Accessor.Tags.AddRangeAsync(tags, cancellationToken);

                SetPopulating();
            }

            if (!await Accessor.Units.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var units = await Seeder.GetUnitsAsync(cancellationToken);
                var paneIds = (await Seeder.GetPanesAsync(cancellationToken)).Select(s => s.Id);
                var tagNames = (await Seeder.GetTagsAsync(cancellationToken)).Select(s => s.Name);

                Accessor.AddUnits(units, Seeder.IdGeneratorFactory, Seeder.Clock,
                    Seeder.GetInitialUserId()!, paneIds, claims: null, tagNames);

                SetPopulating();
            }

            if (IsPopulated)
            {
                await Accessor.SaveChangesAsync(cancellationToken);
            }
        }

    }
}
