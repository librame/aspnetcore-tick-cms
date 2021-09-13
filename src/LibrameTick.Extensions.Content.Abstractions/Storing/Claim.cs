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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Librame.Extensions.Content.Storing
{
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


        #region Override

        /// <summary>
        /// 比较相等（默认比较类别标识与名称）。
        /// </summary>
        /// <param name="other">给定的 <see cref="Claim"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(Claim? other)
            => other != null && other.Name == Name;

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
}
