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
    /// 集成用户。
    /// </summary>
    [Description("集成用户")]
    public class IntegrationUser : AbstractCreationIdentifier<string, string>, IUser, IEquatable<IntegrationUser>
    {
        /// <summary>
        /// 用户名称。
        /// </summary>
        [Display(Name = nameof(UserName), ResourceType = typeof(PortalResource))]
        public virtual string UserName { get; set; }
            = string.Empty;

        /// <summary>
        /// 密码哈希。
        /// </summary>
        public virtual string PasswordHash { get; set; }
            = string.Empty;


        #region Override

        /// <summary>
        /// 比较相等（默认比较用户名称）。
        /// </summary>
        /// <param name="other">给定的 <see cref="IntegrationUser"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(IntegrationUser? other)
            => other != null && other.UserName == UserName;

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
            => $"{base.ToString()};{nameof(UserName)}={UserName}";

        #endregion

    }
}
