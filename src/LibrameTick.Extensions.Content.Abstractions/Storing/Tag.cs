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

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 标签。
/// </summary>
[Description("标签")]
public class Tag : AbstractCreationIdentifier<int, string>, IEquatable<Tag>
{
    /// <summary>
    /// 名称。
    /// </summary>
    [Display(Name = nameof(Name), ResourceType = typeof(ContentResource))]
    public virtual string Name { get; set; }
        = string.Empty;


    /// <summary>
    /// 单元标签集合。
    /// </summary>
    public virtual List<UnitTag>? UnitTags { get; set; }


    /// <summary>
    /// 添加导航单元声明。
    /// </summary>
    /// <param name="unitTag">给定的 <see cref="UnitTag"/>。</param>
    /// <returns>返回 <see cref="Tag"/>。</returns>
    public virtual Tag AddUnitTag(UnitTag unitTag)
    {
        if (UnitTags is null)
            UnitTags = new();

        UnitTags.Add(unitTag);
        return this;
    }


    #region Override

    /// <summary>
    /// 比较相等（默认比较名称）。
    /// </summary>
    /// <param name="other">给定的 <see cref="Tag"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(Tag? other)
        => other is not null && other.Name == Name;

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
        => $"{base.ToString()};{nameof(Name)}={Name}";

    #endregion

}
