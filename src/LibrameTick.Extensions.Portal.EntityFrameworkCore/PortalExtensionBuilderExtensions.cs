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
using Librame.Extensions.Portal;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="PortalExtensionBuilder"/> 静态扩展。
/// </summary>
public static class PortalExtensionBuilderExtensions
{

    /// <summary>
    /// 添加 <see cref="PortalExtensionBuilder"/>。
    /// </summary>
    /// <param name="contentBuilder">给定的 <see cref="ContentExtensionBuilder"/>。</param>
    /// <param name="setupAction">给定的配置选项动作（可选）。</param>
    /// <returns>返回 <see cref="PortalExtensionBuilder"/>。</returns>
    public static PortalExtensionBuilder AddPortal(this ContentExtensionBuilder contentBuilder,
        Action<PortalExtensionOptions>? setupAction = null)
    {
        var options = new PortalExtensionOptions(contentBuilder.Options);
        options.TryLoadOptionsFromJson();

        setupAction?.Invoke(options);

        return new PortalExtensionBuilder(contentBuilder, options);
    }

}
