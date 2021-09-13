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
        private const string GetUnitsKey = "GetInitialUnits";


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
        public IClock Clock { get; init; }


        /// <summary>
        /// 获取初始用户标识。
        /// </summary>
        /// <returns>返回标识字符串。</returns>
        public abstract string? GetInitialUserId();


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
        /// <returns>返回 <see cref="IEnumerable{Category}"/>。</returns>
        public IEnumerable<Category> GetCategories()
        {
            return (IEnumerable<Category>)SeedBank.GetOrAdd(GetCategoriesKey, key =>
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
        /// <returns>返回一个包含 <see cref="IEnumerable{Category}"/> 的异步操作。</returns>
        public Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetCategories);


        /// <summary>
        /// 获取声明集合。
        /// </summary>
        /// <returns>返回 <see cref="IEnumerable{Claim}"/>。</returns>
        public IEnumerable<Claim> GetClaims()
        {
            return (IEnumerable<Claim>)SeedBank.GetOrAdd(GetClaimsKey, key =>
            {
                return ContentOptions.InitialClaims.Select(pair =>
                {
                    var claim = new Claim();

                    claim.Name = pair.Key;
                    claim.Description = pair.Value;

                    claim.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return claim;
                });
            });
        }

        /// <summary>
        /// 异步获取声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="IEnumerable{Claim}"/> 的异步操作。</returns>
        public Task<IEnumerable<Claim>> GetClaimsAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetClaims);


        /// <summary>
        /// 获取窗格集合。
        /// </summary>
        /// <returns>返回 <see cref="IEnumerable{Pane}"/>。</returns>
        public IEnumerable<Pane> GetPanes()
        {
            return (IEnumerable<Pane>)SeedBank.GetOrAdd(GetPanesKey, key =>
            {
                var categories = GetCategories();

                return ContentOptions.InitialPanes.Select(pair =>
                {
                    var pane = new Pane();

                    pane.CategoryId = categories.First(p => p.Name == pair.Value.Category).Id;
                    pane.Name = pair.Key;
                    pane.Description = pair.Value.Description;
                    pane.Template = pair.Value.Template;

                    pane.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return pane;
                });
            });
        }

        /// <summary>
        /// 异步获取窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="IEnumerable{Pane}"/> 的异步操作。</returns>
        public Task<IEnumerable<Pane>> GetPanesAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetPanes);


        /// <summary>
        /// 获取来源集合。
        /// </summary>
        /// <returns>返回 <see cref="IEnumerable{Source}"/>。</returns>
        public IEnumerable<Source> GetSources()
        {
            return (IEnumerable<Source>)SeedBank.GetOrAdd(GetSourcesKey, key =>
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
        /// <returns>返回一个包含 <see cref="IEnumerable{Source}"/> 的异步操作。</returns>
        public Task<IEnumerable<Source>> GetSourcesAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetSources);


        /// <summary>
        /// 获取标签集合。
        /// </summary>
        /// <returns>返回 <see cref="IEnumerable{Tag}"/>。</returns>
        public IEnumerable<Tag> GetTags()
        {
            return (IEnumerable<Tag>)SeedBank.GetOrAdd(GetTagsKey, key =>
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
        /// <returns>返回一个包含 <see cref="IEnumerable{Tag}"/> 的异步操作。</returns>
        public Task<IEnumerable<Tag>> GetTagsAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetTags);


        /// <summary>
        /// 获取单元集合。
        /// </summary>
        /// <returns>返回 <see cref="List{Unit}"/>。</returns>
        public List<Unit> GetUnits()
        {
            return (List<Unit>)SeedBank.GetOrAdd(GetUnitsKey, key =>
            {
                var categories = GetCategories();
                var sources = GetSources();

                var units = new List<Unit>();

                foreach (var pane in GetPanes())
                {
                    for (var i = 1; i < 21; i++)
                    {
                        var unit = new Unit();

                        unit.Id = IdGeneratorFactory.GetNewId<long>();
                        unit.CategoryId = categories.First(p => p.Name == pane.Name).Id;
                        unit.SourceId = sources.First().Id;
                        unit.Title = $"测试{pane.Name}单元标题{i}";
                        unit.Cover = "images/default.jpg";

                        for (var j = 0; j < 10; j++)
                        {
                            unit.Body += $"测试{pane.Name}单元内容{i}。";
                        }

                        unit.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                        units.Add(unit);
                    }
                }

                return units;
            });
        }

        /// <summary>
        /// 异步获取单元集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="List{Unit}"/> 的异步操作。</returns>
        public Task<List<Unit>> GetUnitsAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetUnits);

    }
}
