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
using System.ComponentModel.DataAnnotations;

namespace Librame.Extensions.Content.Storing
{
    /// <summary>
    /// 单元标签。
    /// </summary>
    public class UnitTag : AbstractCreationIdentifier<long, string>, IEquatable<UnitTag>
    {
        /// <summary>
        /// 单元标识。
        /// </summary>
        [Display(Name = nameof(UnitId), ResourceType = typeof(ContentResource))]
        public virtual long UnitId { get; set; }

        /// <summary>
        /// 标签标识。
        /// </summary>
        [Display(Name = nameof(TagId), ResourceType = typeof(ContentResource))]
        public virtual int TagId { get; set; }


        #region Override

        /// <summary>
        /// 比较相等（默认比较单元标识与标签标识）。
        /// </summary>
        /// <param name="other">给定的 <see cref="UnitTag"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(UnitTag? other)
            => other != null && other.UnitId == UnitId && other.TagId == TagId;

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
            => $"{base.ToString()};{nameof(UnitId)}={UnitId};{nameof(TagId)}={TagId}";

        #endregion

    }
}
