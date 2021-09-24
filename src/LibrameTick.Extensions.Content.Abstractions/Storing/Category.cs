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
using Librame.Extensions.Resources;

namespace Librame.Extensions.Content.Storing;

/// <summary>
/// 类别。
/// </summary>
[Description("类别")]
public class Category : AbstractCreationIdentifier<int, string>, IParentIdentifier<int>, IEquatable<Category>
{
    /// <summary>
    /// 父标识。
    /// </summary>
    [Display(Name = nameof(ParentId), GroupName = "GlobalGroup", ResourceType = typeof(DataResource))]
    public virtual int ParentId { get; set; }

    /// <summary>
    /// 名称。
    /// </summary>
    [Display(Name = nameof(Name), ResourceType = typeof(ContentResource))]
    public virtual string Name { get; set; }
        = string.Empty;

    /// <summary>
    /// 描述。
    /// </summary>
    [Display(Name = nameof(Description), ResourceType = typeof(ContentResource))]
    public virtual string? Description { get; set; }


    #region IObjectIdentifier

    /// <summary>
    /// 获取对象父级标识。
    /// </summary>
    /// <returns>返回标识对象。</returns>
    public object GetObjectParentId()
        => ParentId;

    /// <summary>
    /// 异步获取对象父级标识。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
    /// <returns>返回一个包含标识对象的异步操作。</returns>
    public ValueTask<object> GetObjectParentIdAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunValueTask(GetObjectParentId);


    /// <summary>
    /// 设置对象父级标识。
    /// </summary>
    /// <param name="newParentId">给定的新父级标识。</param>
    /// <returns>返回标识对象。</returns>
    public object SetObjectParentId(object newParentId)
    {
        ParentId = ToId(newParentId, nameof(newParentId));
        return newParentId;
    }

    /// <summary>
    /// 异步设置对象父级标识。
    /// </summary>
    /// <param name="newParentId">给定的新父级标识。</param>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
    /// <returns>返回一个包含标识对象的异步操作。</returns>
    public ValueTask<object> SetObjectParentIdAsync(object newParentId, CancellationToken cancellationToken = default)
        => cancellationToken.RunValueTask(() => SetObjectParentId(newParentId));

    #endregion


    #region Override

    /// <summary>
    /// 比较相等（默认比较父级标识与名称）。
    /// </summary>
    /// <param name="other">给定的 <see cref="Category"/>。</param>
    /// <returns>返回布尔值。</returns>
    public bool Equals(Category? other)
        => other is not null && other.ParentId == ParentId && other.Name == Name;

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
        => $"{base.ToString()};{nameof(ParentId)}={ParentId};{nameof(Name)}={Name}";

    #endregion

}
