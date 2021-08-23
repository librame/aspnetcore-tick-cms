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
    /// <see cref="PortalExtensionBuilder"/> 静态扩展。
    /// </summary>
    public static class PortalExtensionBuilderExtensions
    {

        /// <summary>
        /// 添加 <see cref="PortalExtensionBuilder"/>。
        /// </summary>
        /// <param name="dataBuilder">给定的 <see cref="DataExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的配置选项动作（可选）。</param>
        /// <param name="tryLoadOptionsFromJson">尝试从本地 JSON 文件中加载选项配置（可选；默认不加载）。</param>
        /// <returns>返回 <see cref="PortalExtensionBuilder"/>。</returns>
        public static PortalExtensionBuilder AddPortal(this DataExtensionBuilder dataBuilder,
            Action<PortalExtensionOptions>? setupAction = null, bool tryLoadOptionsFromJson = false)
        {
            var options = new PortalExtensionOptions(dataBuilder.Options);

            if (tryLoadOptionsFromJson)
                options.TryLoadOptionsFromJson(); // 强迫症，默认不初始创建暂不需要的文件夹

            setupAction?.Invoke(options);

            return new PortalExtensionBuilder(dataBuilder, options);
        }

    }
}
