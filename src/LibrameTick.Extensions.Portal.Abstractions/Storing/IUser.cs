#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal.Storing
{
    /// <summary>
    /// 定义用户接口（兼容 Microsoft.AspNetCore.Identity.IdentityUser）。
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// 用户名称。
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 密码哈希。
        /// </summary>
        string PasswordHash { get; set; }
    }
}
