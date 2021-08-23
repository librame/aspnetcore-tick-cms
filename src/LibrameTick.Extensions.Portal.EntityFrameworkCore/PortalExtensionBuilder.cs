#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Core;
using Librame.Extensions.Data;

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
        /// <paramref name="dataBuilder"/> 或 <paramref name="options"/> 为空。
        /// </exception>
        /// <param name="dataBuilder">给定的 <see cref="DataExtensionBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="PortalExtensionOptions"/>。</param>
        public PortalExtensionBuilder(DataExtensionBuilder dataBuilder, PortalExtensionOptions options)
            : base(dataBuilder, options)
        {
            TryAddOrReplaceServiceByCharacteristic<IPasswordHasher, InternalPasswordHasher>();
        }

    }
}
