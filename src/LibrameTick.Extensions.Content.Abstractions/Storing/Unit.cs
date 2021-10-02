#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Resources;
using Librame.Extensions.Data;
using Librame.Extensions.Data.Sharding;

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 单元。
/// </summary>
[Description("单元")]
[Sharded(typeof(DateTimeShardingStrategy), "%y")]
public class Unit : AbstractPublicationIdentifier<long, string>, IEquatable<Unit>
{
    /// <summary>
    /// 类别标识。
    /// </summary>
    [Display(Name = nameof(CategoryId), ResourceType = typeof(ContentResource))]
    public virtual int CategoryId { get; set; }

    /// <summary>
    /// 来源标识。
    /// </summary>
    [Display(Name = nameof(SourceId), ResourceType = typeof(ContentResource))]
    public virtual int SourceId { get; set; }

    /// <summary>
    /// 标题。
    /// </summary>
    [Display(Name = nameof(Title), ResourceType = typeof(ContentResource))]
    public virtual string Title { get; set; }
        = string.Empty;

    /// <summary>
    /// 副标题。
    /// </summary>
    [Display(Name = nameof(Subtitle), ResourceType = typeof(ContentResource))]
    public virtual string? Subtitle { get; set; }

    /// <summary>
    /// 引用源。
    /// </summary>
    [Display(Name = nameof(Reference), ResourceType = typeof(ContentResource))]
    public virtual string? Reference { get; set; }

    /// <summary>
    /// 封面。
    /// </summary>
    [Display(Name = nameof(Cover), ResourceType = typeof(ContentResource))]
    public virtual string? Cover { get; set; }

    /// <summary>
    /// 主体。
    /// </summary>
    [Display(Name = nameof(Body), ResourceType = typeof(ContentResource))]
    public virtual string Body { get; set; }
        = string.Empty;


    /// <summary>
    /// 分类。
    /// </summary>
    [JsonIgnore]
    public virtual Category? Category {  get; set; }

    /// <summary>
    /// 来源。
    /// </summary>
    [JsonIgnore]
    public virtual Source? Source { get; set; }

    /// <summary>
    /// 访问计数。
    /// </summary>
    public virtual UnitVisitCount? VisitCount { get; set; }

    /// <summary>
    /// 声明集合。
    /// </summary>
    public virtual List<UnitClaim>? Claims { get; set; }

    /// <summary>
    /// 标签集合。
    /// </summary>
    public virtual List<UnitTag>? Tags { get; set; }

    /// <summary>
    /// 窗格单元集合。
    /// </summary>
    public virtual List<PaneUnit>? PaneUnits { get; set; }


    /// <summary>
    /// 添加导航单元声明。
    /// </summary>
    /// <param name="unitClaim">给定的 <see cref="UnitClaim"/>。</param>
    /// <returns>返回 <see cref="Unit"/>。</returns>
    public virtual Unit AddClaim(UnitClaim unitClaim)
    {
        if (Claims is null)
            Claims = new();

        Claims.Add(unitClaim);
        return this;
    }

    /// <summary>
    /// 添加导航单元标签。
    /// </summary>
    /// <param name="unitTag">给定的 <see cref="UnitTag"/>。</param>
    /// <returns>返回 <see cref="Unit"/>。</returns>
    public virtual Unit AddTag(UnitTag unitTag)
    {
        if (Tags is null)
            Tags = new();

        Tags.Add(unitTag);
        return this;
    }

    /// <summary>
    /// 添加导航窗格单元。
    /// </summary>
    /// <param name="paneUnit">给定的 <see cref="PaneUnit"/>。</param>
    /// <returns>返回 <see cref="Unit"/>。</returns>
    public virtual Unit AddPaneUnit(PaneUnit paneUnit)
    {
        if (PaneUnits is null)
            PaneUnits = new();

        PaneUnits.Add(paneUnit);
        return this;
    }

    /// <summary>
    /// 初始化访问计数。
    /// </summary>
    /// <returns>返回 <see cref="Unit"/>。</returns>
    public virtual Unit InitialVisitCount()
    {
        VisitCount = new UnitVisitCount
        {
            Unit = this
        };
        return this;
    }


    #region Override

    /// <summary>
    /// 比较相等（默认比较类型标识与标题）。
    /// </summary>
    /// <param name="other">给定的 <see cref="Unit"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(Unit? other)
        => other is not null && other.CategoryId == CategoryId && other.Title == Title;

    /// <summary>
    /// 获取哈希码。
    /// </summary>
    /// <returns>返回 32 位整数。</returns>
    public override int GetHashCode()
        => ToString().GetHashCode();

    /// <summary>
    /// 转换为字符串。
    /// </summary>
    /// <returns>返回字符串。</returns>
    public override string ToString()
        => $"{base.ToString()};{nameof(CategoryId)}={CategoryId};{nameof(Title)}={Title}";

    #endregion

}
