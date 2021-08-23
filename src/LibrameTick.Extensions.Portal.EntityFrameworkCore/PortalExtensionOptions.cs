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
using Librame.Extensions.Data.Cryptography;
using System.Text.Json.Serialization;

namespace Librame.Extensions.Portal
{
    /// <summary>
    /// 门户扩展选项。
    /// </summary>
    public class PortalExtensionOptions : AbstractExtensionOptions<PortalExtensionOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalExtensionOptions"/>。
        /// </summary>
        /// <param name="dataOptions">给定的 <see cref="DataExtensionOptions"/>。</param>
        public PortalExtensionOptions(DataExtensionOptions dataOptions)
            : base(dataOptions, dataOptions.Directories)
        {
            DataOptions = dataOptions;

            ServiceCharacteristics.AddSingleton<IPasswordHasher>();
        }


        /// <summary>
        /// 数据扩展选项。
        /// </summary>
        [JsonIgnore]
        public DataExtensionOptions DataOptions { get; init; }

        /// <summary>
        /// 映射关系（默认不映射）。
        /// </summary>
        public bool MapRelationship { get; set; }

        /// <summary>
        /// 初始编者字典集合（键表示名称，值表示描述）。
        /// </summary>
        public Dictionary<string, string> InitialEditors { get; set; }
            = new Dictionary<string, string>
            {
                { "小编", "默认小编" }
            };

        /// <summary>
        /// 初始集成用户列表集合（键表示用户名称，值表示密码；密码可为空，如果为空则使用初始密码）。
        /// </summary>
        public Dictionary<string, string?> InitialIntegrationUsers { get; set; }
            = new Dictionary<string, string?>
            {
                { "admin", null }
            };

        /// <summary>
        /// 初始密码。
        /// </summary>
        [Encrypted]
        public string InitialPassword { get; set; }
            = "admin666";

    }
}
