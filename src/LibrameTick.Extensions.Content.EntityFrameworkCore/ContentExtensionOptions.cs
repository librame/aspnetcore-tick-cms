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
using System.Text.Json.Serialization;

namespace Librame.Extensions.Content
{
    /// <summary>
    /// 内容扩展选项。
    /// </summary>
    public class ContentExtensionOptions : AbstractExtensionOptions<ContentExtensionOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ContentExtensionOptions"/>。
        /// </summary>
        /// <param name="dataOptions">给定的 <see cref="DataExtensionOptions"/>。</param>
        public ContentExtensionOptions(DataExtensionOptions dataOptions)
            : base(dataOptions, dataOptions.Directories)
        {
            DataOptions = dataOptions;
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
        /// 初始类别字典集合。
        /// </summary>
        public Dictionary<string, (string? ParentName, string? Description)> InitialCategories { get; set; }
            = new Dictionary<string, (string? ParentName, string? Description)>
            {
                { "文章", ( null, "文章类别 (Article)" ) },
                { "文集", ( null, "文集类别 (Anthology)" ) },
                { "图片", ( null, "图片类别 (Picture)" ) },
                { "图册", ( null, "图册类别 (Album)" ) },
                { "专题", ( null, "专题类别 (Subject)" ) }
            };

        /// <summary>
        /// 初始声明字典集合。
        /// </summary>
        public Dictionary<string, string> InitialClaims { get; set; }
            = new Dictionary<string, string>
            {
                { "正文", "正文声明 (Text)"  },
                { "模板", "模板声明 (Template)" }
            };

        /// <summary>
        /// 初始窗格字典集合。
        /// </summary>
        public Dictionary<string, (string Category, string Description, string Template)> InitialPanes { get; set; }
            = new Dictionary<string, (string Category, string Description, string Template)>
            {
                { "快讯", ( "文章", "最新消息", "* {Title}" ) },
                { "焦点", ( "文章", "最热排行", "{Number}. {Title}" ) }
            };

        /// <summary>
        /// 初始来源字典集合。
        /// </summary>
        public Dictionary<string, (string? ParentName, string? Description)> InitialSources { get; set; }
            = new Dictionary<string, (string? ParentName, string? Description)>
            {
                { "原创", ( null, "本站原创" ) }
            };

        /// <summary>
        /// 初始标签列表集合。
        /// </summary>
        public List<string> InitialTags { get; set; }
            = new List<string>
            {
                "测试标签"
            };

    }
}
