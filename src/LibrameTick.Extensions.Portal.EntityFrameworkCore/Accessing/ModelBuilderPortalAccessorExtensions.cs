#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data;
using Librame.Extensions.Portal.Storing;
using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Portal.Accessing
{
    /// <summary>
    /// <see cref="ModelBuilder"/> 门户访问器静态扩展。
    /// </summary>
    public static class ModelBuilderPortalAccessorExtensions
    {

        /// <summary>
        /// 创建门户模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <returns>返回 <see cref="ModelBuilder"/>。</returns>
        public static ModelBuilder CreatePortalModel(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Editor>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UserId, i.Name }).IsUnique();

                b.Property(p => p.Name).HasMaxLength(100);
                b.Property(p => p.Description).HasMaxLength(250);
                b.Property(p => p.Portrait).HasMaxLength(250);
            });

            modelBuilder.Entity<IntegrationUser>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.UserName).IsUnique();

                b.Property(p => p.UserName).HasMaxLength(100);
            });

            return modelBuilder;
        }

    }
}
