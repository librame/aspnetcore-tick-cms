#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content;
using Librame.Extensions.Core;

namespace Librame.Extensions.Portal
{
    /// <summary>
    /// 门户扩展构建器。
    /// </summary>
    public class PortalExtensionBuilder : AbstractExtensionBuilder<PortalExtensionOptions, PortalExtensionBuilder>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalExtensionBuilder"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="contentBuilder"/> 或 <paramref name="options"/> 为空。
        /// </exception>
        /// <param name="contentBuilder">给定的 <see cref="ContentExtensionBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="PortalExtensionOptions"/>。</param>
        public PortalExtensionBuilder(ContentExtensionBuilder contentBuilder, PortalExtensionOptions options)
            : base(contentBuilder, options)
        {
            TryAddOrReplaceService(typeof(IPasswordHasher<>), typeof(InternalPasswordHasher<>));
        }

    }
}
