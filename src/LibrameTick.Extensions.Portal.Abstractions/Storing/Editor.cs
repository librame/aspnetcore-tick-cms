#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data;
using Librame.Extensions.Portal.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Librame.Extensions.Portal.Storing
{
    /// <summary>
    /// 编者。
    /// </summary>
    [Description("编者")]
    public class Editor : AbstractCreationIdentifier<int, string>, IEquatable<Editor>
    {

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// 用户标识。
        /// </summary>
        [Display(Name = nameof(UserId), ResourceType = typeof(PortalResource))]
        public virtual string UserId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalResource))]
        public virtual string Name { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        [Display(Name = nameof(Description), ResourceType = typeof(PortalResource))]
        public virtual string Description { get; set; }

        /// <summary>
        /// 肖像。
        /// </summary>
        [Display(Name = nameof(Name), ResourceType = typeof(PortalResource))]
        public virtual string Portrait { get; set; }

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。


        #region Override

        /// <summary>
        /// 比较相等（默认比较用户标识与名称）。
        /// </summary>
        /// <param name="other">给定的 <see cref="Editor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(Editor? other)
            => other != null && other.UserId == UserId && other.Name == Name;

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
            => $"{base.ToString()};{nameof(UserId)}={UserId};{nameof(Name)}={Name}";

        #endregion

    }
}
