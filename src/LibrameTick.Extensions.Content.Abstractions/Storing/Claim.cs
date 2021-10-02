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

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 声明。
/// </summary>
[Description("声明")]
public class Claim : AbstractCreationIdentifier<int, string>, IEquatable<Claim>
{
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
    /// 窗格声明集合。
    /// </summary>
    public virtual List<PaneClaim>? PaneClaims { get; set; }

    /// <summary>
    /// 单元声明集合。
    /// </summary>
    public virtual List<UnitClaim>? UnitClaims { get; set; }


    /// <summary>
    /// 添加导航窗格声明。
    /// </summary>
    /// <param name="paneClaim">给定的 <see cref="PaneClaim"/>。</param>
    /// <returns>返回 <see cref="Claim"/>。</returns>
    public virtual Claim AddPaneClaim(PaneClaim paneClaim)
    {
        if (PaneClaims is null)
            PaneClaims = new();

        PaneClaims.Add(paneClaim);
        return this;
    }

    /// <summary>
    /// 添加导航窗格声明。
    /// </summary>
    /// <param name="paneClaim">给定的 <see cref="UnitClaim"/>。</param>
    /// <returns>返回 <see cref="Claim"/>。</returns>
    public virtual Claim AddUnitClaim(UnitClaim paneClaim)
    {
        if (UnitClaims is null)
            UnitClaims = new();

        UnitClaims.Add(paneClaim);
        return this;
    }


    #region Override

    /// <summary>
    /// 比较相等（默认比较类别标识与名称）。
    /// </summary>
    /// <param name="other">给定的 <see cref="Claim"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(Claim? other)
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
