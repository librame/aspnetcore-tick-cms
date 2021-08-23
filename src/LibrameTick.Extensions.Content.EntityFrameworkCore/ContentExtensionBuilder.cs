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

namespace Librame.Extensions.Content
{
    /// <summary>
    /// 内容扩展构建器。
    /// </summary>
    public class ContentExtensionBuilder : AbstractExtensionBuilder<ContentExtensionOptions, ContentExtensionBuilder>
    {
        /// <summary>
        /// 构造一个 <see cref="ContentExtensionBuilder"/>。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dataBuilder"/> 或 <paramref name="options"/> 为空。
        /// </exception>
        /// <param name="dataBuilder">给定的 <see cref="DataExtensionBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="ContentExtensionOptions"/>。</param>
        public ContentExtensionBuilder(DataExtensionBuilder dataBuilder, ContentExtensionOptions options)
            : base(dataBuilder, options)
        {
        }

    }
}
