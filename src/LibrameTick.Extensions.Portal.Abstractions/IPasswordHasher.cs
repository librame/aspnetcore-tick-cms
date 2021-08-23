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
    public interface IPasswordHasher
    {
        /// <summary>
        /// 计算密码哈希值。
        /// </summary>
        /// <param name="password">给定的密码。</param>
        /// <returns>返回哈希字符串。</returns>
        string HashPassword(string password);

        /// <summary>
        /// 验证密码是否有效。
        /// </summary>
        /// <param name="hashedPassword">给定的密码哈希。</param>
        /// <param name="providedPassword">给定的原始密码。</param>
        /// <returns>返回布尔值。</returns>
        bool VerifyHashedPassword(string hashedPassword, string providedPassword);
    }
}
