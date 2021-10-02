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
using Librame.Extensions.Data.Sharding;

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 单元访问计数。
/// </summary>
[Description("单元访问计数")]
[Sharded(typeof(DateTimeShardingStrategy), "%y")]
public class UnitVisitCount : IEquatable<UnitVisitCount>
{
    /// <summary>
    /// 单元标识。
    /// </summary>
    [Display(Name = nameof(UnitId), ResourceType = typeof(ContentResource))]
    public virtual long UnitId { get; set; }

    /// <summary>
    /// 支持人数。
    /// </summary>
    //[Display(Name = nameof(SupporterCount), ResourceType = typeof(DataResource))]
    public virtual long SupporterCount { get; set; }

    /// <summary>
    /// 反对人数。
    /// </summary>
    //[Display(Name = nameof(ObjectorCount), ResourceType = typeof(DataResource))]
    public virtual long ObjectorCount { get; set; }

    /// <summary>
    /// 收藏人数。
    /// </summary>
    //[Display(Name = nameof(FavoriteCount), ResourceType = typeof(DataResource))]
    public virtual long FavoriteCount { get; set; }

    /// <summary>
    /// 转发次数。
    /// </summary>
    //[Display(Name = nameof(RetweetCount), ResourceType = typeof(DataResource))]
    public virtual long RetweetCount { get; set; }


    /// <summary>
    /// 访问次数。
    /// </summary>
    //[Display(Name = nameof(VisitCount), ResourceType = typeof(DataResource))]
    public virtual long VisitCount { get; set; }

    /// <summary>
    /// 访问人数。
    /// </summary>
    //[Display(Name = nameof(VisitorCount), ResourceType = typeof(DataResource))]
    public virtual long VisitorCount { get; set; }


    /// <summary>
    /// 单元。
    /// </summary>
    [JsonIgnore]
    public virtual Unit? Unit { get; set; }


    #region Override

    /// <summary>
    /// 比较相等（默认比较单元标识）。
    /// </summary>
    /// <param name="other">给定的 <see cref="UnitVisitCount"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(UnitVisitCount? other)
        => other is not null && other.UnitId == UnitId;

    /// <summary>
    /// 获取哈希码。
    /// </summary>
    /// <returns>返回 32 位整数。</returns>
    public override int GetHashCode()
        => UnitId.GetHashCode();

    /// <summary>
    /// 转换为字符串。
    /// </summary>
    /// <returns>返回字符串。</returns>
    public override string ToString()
        => $"{base.ToString()};{nameof(UnitId)}={UnitId};{nameof(SupporterCount)}={SupporterCount};{nameof(ObjectorCount)}={ObjectorCount};{nameof(FavoriteCount)}={FavoriteCount};{nameof(RetweetCount)}={RetweetCount};{nameof(VisitCount)}={VisitCount};{nameof(VisitorCount)}={VisitorCount}";

    #endregion

}
