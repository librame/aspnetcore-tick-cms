#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Storing;
using Librame.Extensions.Data.Accessing;
using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// 定义内容访问器接口。
    /// </summary>
    public interface IContentAccessor : IAccessor
    {
        /// <summary>
        /// 类别数据集。
        /// </summary>
        DbSet<Category> Categories { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        DbSet<Source> Sources { get; set; }

        /// <summary>
        /// 声明数据集。
        /// </summary>
        DbSet<Claim> Claims { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// 单元数据集。
        /// </summary>
        DbSet<Unit> Units { get; set; }

        /// <summary>
        /// 单元声明数据集。
        /// </summary>
        DbSet<UnitClaim> UnitClaims { get; set; }

        /// <summary>
        /// 单元标签数据集。
        /// </summary>
        DbSet<UnitTag> UnitTags { get; set; }

        /// <summary>
        /// 单元统计数据集。
        /// </summary>
        DbSet<UnitVisitCount> UnitVisitCounts { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        DbSet<Pane> Panes { get; set; }

        /// <summary>
        /// 窗格单元数据集。
        /// </summary>
        DbSet<PaneClaim> PaneClaims { get; set; }
    }
}
