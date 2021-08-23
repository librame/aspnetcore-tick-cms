#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Storing;
using Librame.Extensions.Core;
using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// 定义抽象实现内容 <see cref="IAccessorSeeder"/>。
    /// </summary>
    public abstract class AbstractContentAccessorSeeder : AbstractAccessorSeeder
    {
        private const string GetCategoriesKey = "GetInitialCategories";
        private const string GetClaimsKey = "GetInitialClaims";
        private const string GetPanesKey = "GetInitialPanes";
        private const string GetSourcesKey = "GetInitialSources";
        private const string GetTagsKey = "GetInitialTags";


        /// <summary>
        /// 构造一个 <see cref="AbstractContentAccessorSeeder"/>。
        /// </summary>
        /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="contentOptions">给定的 <see cref="ContentExtensionOptions"/>。</param>
        protected AbstractContentAccessorSeeder(IIdentificationGeneratorFactory idGeneratorFactory,
            ContentExtensionOptions contentOptions)
            : base(idGeneratorFactory)
        {
            ContentOptions = contentOptions;
            Clock = contentOptions.DataOptions.CoreOptions.Clock;
        }


        /// <summary>
        /// 内容扩展选项。
        /// </summary>
        protected ContentExtensionOptions ContentOptions { get; init; }

        /// <summary>
        /// 时钟。
        /// </summary>
        protected IClock Clock { get; init; }


        /// <summary>
        /// 获取初始用户标识。
        /// </summary>
        /// <returns>返回标识字符串。</returns>
        protected abstract string? GetInitialUserId();


        /// <summary>
        /// 获取指定元素集合的累加增量标识。
        /// </summary>
        /// <typeparam name="TElement">指定的元素类型。</typeparam>
        /// <param name="elements">给定的元素集合。</param>
        /// <param name="predicate">断定当前元素的方法。</param>
        /// <returns>返回 32 位整数。</returns>
        protected virtual int GetProgressiveIncremId<TElement>(IEnumerable<TElement> elements,
            Func<TElement, bool> predicate)
        {
            var incremId = 0;

            if (!elements.Any())
                return incremId;

            var index = 0;
            foreach (var element in elements)
            {
                incremId = ++index;

                if (predicate.Invoke(element))
                    break;
            }

            return incremId;
        }


        /// <summary>
        /// 获取类别集合。
        /// </summary>
        /// <returns>返回 <see cref="Category"/> 数组。</returns>
        public Category[] GetCategories()
        {
            return (Category[])SeedBank.GetOrAdd(GetCategoriesKey, key =>
            {
                return ContentOptions.InitialCategories.Select(pair =>
                {
                    var category = new Category();

                    category.Name = pair.Key;
                    category.Description = pair.Value.Description;

                    // 查找与父级名称匹配的对应增量标识
                    var currentParentName = pair.Value.ParentName;

                    category.ParentId = GetProgressiveIncremId(ContentOptions.InitialCategories,
                        ele => ele.Value.ParentName == currentParentName);

                    category.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return category;
                });
            });
        }

        /// <summary>
        /// 异步获取类别集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="Category"/> 数组的异步操作。</returns>
        public Task<Category[]> GetCategoriesAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetCategories);


        /// <summary>
        /// 获取声明集合。
        /// </summary>
        /// <returns>返回 <see cref="Claim"/> 数组。</returns>
        public Claim[] GetClaims()
        {
            return (Claim[])SeedBank.GetOrAdd(GetClaimsKey, key =>
            {
                return ContentOptions.InitialClaims.Select(pair =>
                {
                    var claim = new Claim();

                    claim.Name = pair.Key;
                    claim.Description = pair.Value.Description;

                    // 查找与类别名称匹配的对应增量标识
                    var currentCategoryName = pair.Value.CategoryName;

                    claim.CategoryId = GetProgressiveIncremId(ContentOptions.InitialCategories,
                        ele => ele.Value.ParentName == currentCategoryName);

                    claim.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return claim;
                });
            });
        }

        /// <summary>
        /// 异步获取声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="Claim"/> 数组的异步操作。</returns>
        public Task<Claim[]> GetClaimsAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetClaims);


        /// <summary>
        /// 获取窗格集合。
        /// </summary>
        /// <returns>返回 <see cref="Pane"/> 数组。</returns>
        public Pane[] GetPanes()
        {
            return (Pane[])SeedBank.GetOrAdd(GetPanesKey, key =>
            {
                return ContentOptions.InitialPanes.Select(pair =>
                {
                    var pane = new Pane();

                    pane.Name = pair.Key;
                    pane.Description = pair.Value.Description;

                    // 查找与父级名称匹配的对应增量标识
                    var currentParentName = pair.Value.ParentName;

                    pane.ParentId = GetProgressiveIncremId(ContentOptions.InitialPanes,
                        ele => ele.Value.ParentName == currentParentName);

                    pane.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return pane;
                });
            });
        }

        /// <summary>
        /// 异步获取窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="Pane"/> 数组的异步操作。</returns>
        public Task<Pane[]> GetPanesAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetPanes);


        /// <summary>
        /// 获取来源集合。
        /// </summary>
        /// <returns>返回 <see cref="Source"/> 数组。</returns>
        public Source[] GetSources()
        {
            return (Source[])SeedBank.GetOrAdd(GetSourcesKey, key =>
            {
                return ContentOptions.InitialSources.Select(pair =>
                {
                    var source = new Source();

                    source.Name = pair.Key;
                    source.Description = pair.Value.Description;

                    // 查找与父级名称匹配的对应增量标识
                    var currentParentName = pair.Value.ParentName;

                    source.ParentId = GetProgressiveIncremId(ContentOptions.InitialSources,
                        ele => ele.Value.ParentName == currentParentName);

                    source.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return source;
                });
            });
        }

        /// <summary>
        /// 异步获取来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="Source"/> 数组的异步操作。</returns>
        public Task<Source[]> GetSourcesAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetSources);


        /// <summary>
        /// 获取标签集合。
        /// </summary>
        /// <returns>返回 <see cref="Tag"/> 数组。</returns>
        public Tag[] GetTags()
        {
            return (Tag[])SeedBank.GetOrAdd(GetTagsKey, key =>
            {
                return ContentOptions.InitialTags.Select(name =>
                {
                    var tag = new Tag();

                    tag.Name = name;

                    tag.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return tag;
                });
            });
        }

        /// <summary>
        /// 异步获取标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="Tag"/> 数组的异步操作。</returns>
        public Task<Tag[]> GetTagsAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetTags);

    }
}
