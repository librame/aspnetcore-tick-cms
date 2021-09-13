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
        /// <param name="contentOptions">给定的 <see cref="ContentExtensionOptions"/>。</param>
        public PortalExtensionOptions(ContentExtensionOptions contentOptions)
            : base(contentOptions, contentOptions.Directories)
        {
            ContentOptions = contentOptions;

            ServiceCharacteristics.AddSingleton(typeof(IPasswordHasher<>));
        }


        /// <summary>
        /// 内容扩展选项。
        /// </summary>
        [JsonIgnore]
        public ContentExtensionOptions ContentOptions { get; init; }

        /// <summary>
        /// 映射关系（默认不映射）。
        /// </summary>
        public bool MapRelationship { get; set; }

        /// <summary>
        /// 初始编者字典集合（键表示名称，值元组分别表示用户名称、编者备注）。
        /// </summary>
        public Dictionary<string, (string UserName, string? Description)> InitialEditors { get; set; }
            = new Dictionary<string, (string UserName, string? Description)>
            {
                { "主编", ( "admin", "初始主编" ) }
            };

        /// <summary>
        /// 初始用户列表集合（键表示用户名称，值表示密码；密码可为空，如果为空则使用初始密码）。
        /// </summary>
        public Dictionary<string, string?> InitialUsers { get; set; }
            = new Dictionary<string, string?>
            {
                { "admin", "admin666" }
            };

        /// <summary>
        /// 初始密码。
        /// </summary>
        [Encrypted]
        public string InitialPassword { get; set; }
            = "123456";

    }
}
