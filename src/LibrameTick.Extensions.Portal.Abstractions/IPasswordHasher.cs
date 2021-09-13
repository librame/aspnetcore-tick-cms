#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Extensions.Portal
{
    /// <summary>
    /// 定义密码哈希计算器接口。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    public interface IPasswordHasher<TUser>
    {
        /// <summary>
        /// 计算密码哈希值。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="password">给定的密码。</param>
        /// <returns>返回哈希字符串。</returns>
        string HashPassword(TUser user, string password);

        /// <summary>
        /// 验证密码是否有效。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <param name="hashedPassword">给定的密码哈希。</param>
        /// <param name="providedPassword">给定的原始密码。</param>
        /// <returns>返回布尔值。</returns>
        bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword);
    }
}
