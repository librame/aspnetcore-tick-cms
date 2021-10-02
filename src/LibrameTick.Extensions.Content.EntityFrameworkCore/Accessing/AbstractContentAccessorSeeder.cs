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
using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;

namespace Librame.Extensions.Content.Accessing;

/// <summary>
/// 定义抽象实现 <see cref="IAccessorSeeder"/> 的内容访问器种子机。
/// </summary>
public abstract class AbstractContentAccessorSeeder : AbstractAccessorSeeder
{
    /// <summary>
    /// 构造一个 <see cref="AbstractContentAccessorSeeder"/>。
    /// </summary>
    /// <param name="contentOptions">给定的 <see cref="ContentExtensionOptions"/>。</param>
    /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
    protected AbstractContentAccessorSeeder(ContentExtensionOptions contentOptions,
        IIdentificationGeneratorFactory idGeneratorFactory)
        : base(contentOptions.DataOptions.CoreOptions.Clock, idGeneratorFactory)
    {
        ContentOptions = contentOptions;
    }


    /// <summary>
    /// 内容扩展选项。
    /// </summary>
    protected ContentExtensionOptions ContentOptions { get; init; }


    /// <summary>
    /// 获取初始用户标识。
    /// </summary>
    /// <returns>返回标识字符串。</returns>
    public abstract string GetInitialUserId();


    /// <summary>
    /// 获取类别集合。
    /// </summary>
    /// <returns>返回 <see cref="List{Category}"/>。</returns>
    public List<Category> GetCategories()
    {
        return Seed(nameof(GetCategories), key =>
        {
            var categories = new List<Category>();

            foreach (var pair in ContentOptions.InitialCategories)
            {
                var category = new Category();

                category.Name = pair.Key;
                category.Description = pair.Value.Description;

                // 查找与父级名称匹配的对应增量标识
                category.ParentId = GetProgressiveIncremId(ContentOptions.InitialCategories,
                    ele => ele.Value.ParentName == pair.Value.ParentName);

                category.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                categories.Add(category);
            }

            return categories;
        });
    }

    /// <summary>
    /// 异步获取类别集合。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
    /// <returns>返回一个包含 <see cref="List{Category}"/> 的异步操作。</returns>
    public Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunTask(GetCategories);


    /// <summary>
    /// 获取声明集合。
    /// </summary>
    /// <returns>返回 <see cref="IEnumerable{Claim}"/>。</returns>
    public IEnumerable<Claim> GetClaims()
    {
        return Seed(nameof(GetClaims), key =>
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
    /// <returns>返回 <see cref="List{Pane}"/>。</returns>
    public List<Pane> GetPanes()
    {
        return Seed(nameof(GetPanes), key =>
        {
            var categories = GetCategories();

            return ContentOptions.InitialPanes.Select(pair =>
            {
                var pane = new Pane();

                pane.Name = pair.Key;
                pane.Description = pair.Value.Description;
                pane.Template = pair.Value.Template;

                //pane.CategoryId = GetProgressiveIncremId(categories, p => p.Name == pair.Value.Category);
                pane.Category = categories.FirstOrDefault(p => p.Name == pair.Value.Category);

                pane.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                if (pane.Category is not null)
                    pane.Category.AddPane(pane);

                return pane;
            }).ToList();
        });
    }

    /// <summary>
    /// 异步获取窗格集合。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
    /// <returns>返回一个包含 <see cref="IEnumerable{Pane}"/> 的异步操作。</returns>
    public Task<List<Pane>> GetPanesAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunTask(GetPanes);


    /// <summary>
    /// 获取来源集合。
    /// </summary>
    /// <returns>返回 <see cref="List{Source}"/>。</returns>
    public List<Source> GetSources()
    {
        return Seed(nameof(GetSources), key =>
        {
            var sources = new List<Source>();

            foreach (var pair in ContentOptions.InitialSources)
            {
                var source = new Source();

                source.Name = pair.Key;
                source.Description = pair.Value.Description;

                // 查找与父级名称匹配的对应增量标识
                source.ParentId = GetProgressiveIncremId(ContentOptions.InitialSources,
                    ele => ele.Value.ParentName == pair.Value.ParentName);

                source.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                sources.Add(source);
            }

            return sources;
        });
    }

    /// <summary>
    /// 异步获取来源集合。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
    /// <returns>返回一个包含 <see cref="List{Source}"/> 的异步操作。</returns>
    public Task<List<Source>> GetSourcesAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunTask(GetSources);


    /// <summary>
    /// 获取标签集合。
    /// </summary>
    /// <returns>返回 <see cref="List{Tag}"/>。</returns>
    public List<Tag> GetTags()
    {
        return Seed(nameof(GetTags), key =>
        {
            return ContentOptions.InitialTags.Select(name =>
            {
                var tag = new Tag();

                tag.Name = name;

                tag.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                return tag;
            }).ToList();
        });
    }

    /// <summary>
    /// 异步获取标签集合。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
    /// <returns>返回一个包含 <see cref="List{Tag}"/> 的异步操作。</returns>
    public Task<List<Tag>> GetTagsAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunTask(GetTags);


    /// <summary>
    /// 获取单元集合。
    /// </summary>
    /// <returns>返回 <see cref="List{Unit}"/>。</returns>
    public List<Unit> GetUnits()
    {
        return Seed(nameof(GetUnits), key =>
        {
            var categories = GetCategories();
            var tags = GetTags();
            var panes = GetPanes();

            var units = new List<Unit>();

            foreach (var pane in panes)
            {
                for (var i = 1; i < 11; i++)
                {
                    var unit = new Unit();

                    unit.Id = IdGeneratorFactory.GetNewId<long>();
                    unit.Title = $"测试{pane.Name}单元标题{i}";
                    unit.Subtitle = unit.Title;
                    unit.Cover = ContentOptions.InitialUnitCover;
                    unit.PublishedAs = $"/preview/{unit.Id}";

                    unit.CategoryId = GetProgressiveIncremId(categories, p => p.Name == pane.Name);
                    unit.SourceId = 1;
                    // 使用实体关联易造成冲突，改为使用标识
                    //unit.Category = categories.FirstOrDefault(p => p.Name == pane.Name);
                    //unit.Source = sources.FirstOrDefault();

                    for (var j = 0; j < 10; j++)
                    {
                        unit.Body += $"测试{pane.Name}单元内容{i}。";
                    }

                    unit.PopulatePublication(GetInitialUserId(), Clock.GetUtcNow());

                    if (unit.Category is not null)
                        unit.Category.AddUnit(unit);

                    if (unit.Source is not null)
                        unit.Source.AddUnit(unit);

                    // Add Tag
                    var tagIndex = RandomExtensions.Run(r => r.Next(tags.Count - 1));
                    var tag = tags[tagIndex];

                    var unitTag = new UnitTag
                    {
                        Id = IdGeneratorFactory.GetNewId<long>(),
                        Unit = unit,
                        Tag = tag
                    };

                    unitTag.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    tag.AddUnitTag(unitTag);
                    unit.AddTag(unitTag);

                    // Add PaneUnit
                    var paneUnit = new PaneUnit
                    {
                        Pane = pane,
                        Unit = unit
                    };

                    paneUnit.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    pane.AddPaneUnit(paneUnit);
                    unit.AddPaneUnit(paneUnit);

                    // Initial VisitCount
                    unit.InitialVisitCount();

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
