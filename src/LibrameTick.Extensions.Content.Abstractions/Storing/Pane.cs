#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Resources;
using Librame.Extensions.Data;
using Librame.Extensions.Resources;

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 窗格。
/// </summary>
[Description("窗格")]
public class Pane : AbstractCreationIdentifier<int, string>, IParentIdentifier<int>
{
    /// <summary>
    /// 父标识。
    /// </summary>
    [Display(Name = nameof(ParentId), GroupName = "GlobalGroup", ResourceType = typeof(DataResource))]
    public virtual int ParentId { get; set; }

    /// <summary>
    /// 类别标识。
    /// </summary>
    [Display(Name = nameof(CategoryId), ResourceType = typeof(ContentResource))]
    public virtual int CategoryId { get; set; }

    /// <summary>
    /// 名称。
    /// </summary>
    [Display(Name = nameof(Name), ResourceType = typeof(ContentResource))]
    public virtual string Name { get; set; }
        = string.Empty;

    /// <summary>
    /// 描述。
    /// </summary>
    [Display(Name = nameof(Description), ResourceType = typeof(ContentResource))]
    public virtual string? Description { get; set; }

    /// <summary>
    /// 图标。
    /// </summary>
    [Display(Name = nameof(Icon), ResourceType = typeof(ContentResource))]
    public virtual string? Icon { get; set; }

    /// <summary>
    /// 更多。
    /// </summary>
    [Display(Name = nameof(More), ResourceType = typeof(ContentResource))]
    public virtual string? More { get; set; }

    /// <summary>
    /// 模板。
    /// </summary>
    [Display(Name = nameof(Template), ResourceType = typeof(ContentResource))]
    public virtual string? Template { get; set; }


    /// <summary>
    /// 分类。
    /// </summary>
    [JsonIgnore]
    public virtual Category? Category {  get; set; }

    /// <summary>
    /// 窗格声明集合。
    /// </summary>
    public virtual List<PaneClaim>? PaneClaims { get; set; }

    /// <summary>
    /// 窗格单元集合。
    /// </summary>
    public virtual List<PaneUnit>? PaneUnits { get; set; }


    /// <summary>
    /// 添加导航窗格声明。
    /// </summary>
    /// <param name="paneClaim">给定的 <see cref="PaneClaim"/>。</param>
    /// <returns>返回 <see cref="Pane"/>。</returns>
    public virtual Pane AddPaneClaim(PaneClaim paneClaim)
    {
        if (PaneClaims is null)
            PaneClaims = new();

        PaneClaims.Add(paneClaim);
        return this;
    }

    /// <summary>
    /// 添加导航窗格单元。
    /// </summary>
    /// <param name="paneUnit">给定的 <see cref="PaneUnit"/>。</param>
    /// <returns>返回 <see cref="Pane"/>。</returns>
    public virtual Pane AddPaneUnit(PaneUnit paneUnit)
    {
        if (PaneUnits is null)
            PaneUnits = new();

        PaneUnits.Add(paneUnit);
        return this;
    }


    #region IObjectIdentifier

    /// <summary>
    /// 获取对象父级标识。
    /// </summary>
    /// <returns>返回标识对象。</returns>
    public object? GetObjectParentId()
        => ParentId;

    /// <summary>
    /// 异步获取对象父级标识。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
    /// <returns>返回一个包含标识对象的异步操作。</returns>
    public ValueTask<object?> GetObjectParentIdAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunValueTask(GetObjectParentId);


    /// <summary>
    /// 设置对象父级标识。
    /// </summary>
    /// <param name="newParentId">给定的新父级标识。</param>
    /// <returns>返回标识对象。</returns>
    public object? SetObjectParentId(object? newParentId)
    {
        ParentId = ToId(newParentId, nameof(newParentId));
        return newParentId;
    }

    /// <summary>
    /// 异步设置对象父级标识。
    /// </summary>
    /// <param name="newParentId">给定的新父级标识。</param>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
    /// <returns>返回一个包含标识对象的异步操作。</returns>
    public ValueTask<object?> SetObjectParentIdAsync(object? newParentId, CancellationToken cancellationToken = default)
        => cancellationToken.RunValueTask(() => SetObjectParentId(newParentId));

    #endregion


    #region Override

    /// <summary>
    /// 比较相等（默认比较父级标识与名称）。
    /// </summary>
    /// <param name="other">给定的 <see cref="Category"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(Category? other)
        => other is not null && other.ParentId == ParentId && other.Name == Name;

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
        => $"{base.ToString()};{nameof(ParentId)}={ParentId};{nameof(Name)}={Name}";

    #endregion

}
