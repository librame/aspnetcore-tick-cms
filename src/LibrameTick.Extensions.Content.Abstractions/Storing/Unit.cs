#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Resources;
using Librame.Extensions.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Librame.Extensions.Content.Storing
{
    /// <summary>
    /// 单元。
    /// </summary>
    [Description("单元")]
    public class Unit : AbstractPublicationIdentifier<long, string>, IEquatable<Unit>
    {
        /// <summary>
        /// 类别标识。
        /// </summary>
        [Display(Name = nameof(CategoryId), ResourceType = typeof(ContentResource))]
        public virtual int CategoryId { get; set; }

        /// <summary>
        /// 来源标识。
        /// </summary>
        [Display(Name = nameof(SourceId), ResourceType = typeof(ContentResource))]
        public virtual int SourceId { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        [Display(Name = nameof(Title), ResourceType = typeof(ContentResource))]
        public virtual string Title { get; set; }
            = string.Empty;

        /// <summary>
        /// 副标题。
        /// </summary>
        [Display(Name = nameof(Subtitle), ResourceType = typeof(ContentResource))]
        public virtual string? Subtitle { get; set; }

        /// <summary>
        /// 引用源。
        /// </summary>
        [Display(Name = nameof(Reference), ResourceType = typeof(ContentResource))]
        public virtual string? Reference { get; set; }

        /// <summary>
        /// 封面。
        /// </summary>
        [Display(Name = nameof(Cover), ResourceType = typeof(ContentResource))]
        public virtual string? Cover { get; set; }

        /// <summary>
        /// 主体。
        /// </summary>
        [Display(Name = nameof(Body), ResourceType = typeof(ContentResource))]
        public virtual string Body { get; set; }
            = string.Empty;


        #region Override

        /// <summary>
        /// 比较相等（默认比较类型标识与标题）。
        /// </summary>
        /// <param name="other">给定的 <see cref="Unit"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(Unit? other)
            => other != null && other.CategoryId == CategoryId && other.Title == Title;

        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{base.ToString()};{nameof(CategoryId)}={CategoryId};{nameof(Title)}={Title}";

        #endregion

    }
}
