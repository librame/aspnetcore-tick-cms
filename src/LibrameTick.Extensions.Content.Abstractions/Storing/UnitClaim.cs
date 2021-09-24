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
using Librame.Extensions.Data.Sharding;

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 单元声明。
/// </summary>
[Description("单元声明")]
[Sharded(typeof(DateTimeShardingStrategy), "%y")]
public class UnitClaim : AbstractCreationIdentifier<long, string>, IEquatable<UnitClaim>
{
    /// <summary>
    /// 单元标识。
    /// </summary>
    [Display(Name = nameof(UnitId), ResourceType = typeof(ContentResource))]
    public virtual long UnitId { get; set; }

    /// <summary>
    /// 声明标识。
    /// </summary>
    [Display(Name = nameof(ClaimId), ResourceType = typeof(ContentResource))]
    public virtual int ClaimId { get; set; }

    /// <summary>
    /// 单元声明值。
    /// </summary>
    [Display(Name = nameof(ClaimValue), ResourceType = typeof(ContentResource))]
    public virtual string ClaimValue { get; set; }
        = string.Empty;


    #region Override

    /// <summary>
    /// 比较相等（默认比较单元标识与声明标识和声明值）。
    /// </summary>
    /// <param name="other">给定的 <see cref="UnitClaim"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(UnitClaim? other)
        => other is not null && other.UnitId == UnitId && other.ClaimId == ClaimId && other.ClaimValue == ClaimValue;

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
        => $"{base.ToString()};{nameof(UnitId)}={UnitId};{nameof(ClaimId)}={ClaimId};{nameof(ClaimValue)}={ClaimValue}";

    #endregion

}
