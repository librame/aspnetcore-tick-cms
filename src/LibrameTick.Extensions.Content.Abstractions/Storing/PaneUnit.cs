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
/// 窗格单元。
/// </summary>
[Description("窗格单元")]
public class PaneUnit : AbstractCreationIdentifier<int, string>, IEquatable<PaneUnit>
{
    /// <summary>
    /// 窗格标识。
    /// </summary>
    [Display(Name = nameof(PaneId), ResourceType = typeof(ContentResource))]
    public virtual int PaneId { get; set; }

    /// <summary>
    /// 单元标识集合。
    /// </summary>
    [Display(Name = nameof(UnitIds), ResourceType = typeof(ContentResource))]
    public virtual string UnitIds { get; set; }
        = string.Empty;


    /// <summary>
    /// 附加单元标识。
    /// </summary>
    /// <param name="unitId">给定的单元标识。</param>
    /// <returns>返回单元标识集合。</returns>
    public string AppendUnitId(long unitId)
    {
        if (!string.IsNullOrEmpty(UnitIds))
            UnitIds += ',';

        return UnitIds += unitId;
    }


    /// <summary>
    /// 分隔单元标识集合。
    /// </summary>
    /// <returns>返回单元标识数组。</returns>
    public long[] SplitUnitIds()
        => SplitUnitIds(UnitIds);

    /// <summary>
    /// 分隔单元标识。
    /// </summary>
    /// <param name="unitIds">给定的单元标识集合。</param>
    /// <returns>返回单元标识数组。</returns>
    public static long[] SplitUnitIds(string unitIds)
    {
        if (string.IsNullOrEmpty(unitIds))
            return Array.Empty<long>();

        return unitIds.Split(',').Select(id => long.Parse(id)).ToArray();
    }


    #region Override

    /// <summary>
    /// 比较相等（默认比较窗格标识与声明标识和声明值）。
    /// </summary>
    /// <param name="other">给定的 <see cref="PaneUnit"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(PaneUnit? other)
        => other is not null && other.PaneId == PaneId && other.UnitIds == UnitIds;

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
        => $"{base.ToString()};{nameof(PaneId)}={PaneId};{nameof(UnitIds)}={UnitIds}";

    #endregion

}
