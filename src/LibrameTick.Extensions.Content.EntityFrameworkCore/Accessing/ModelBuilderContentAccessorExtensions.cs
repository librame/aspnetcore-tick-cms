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
using Librame.Extensions.Data;
using Microsoft.EntityFrameworkCore;

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// <see cref="ModelBuilder"/> 内容访问器静态扩展。
    /// </summary>
    public static class ModelBuilderContentAccessorExtensions
    {

        /// <summary>
        /// 创建内容模型。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="ContentExtensionOptions"/>。</param>
        /// <returns>返回 <see cref="ModelBuilder"/>。</returns>
        public static ModelBuilder CreateContentModel(this ModelBuilder modelBuilder, ContentExtensionOptions options)
        {
            modelBuilder.Entity<Category>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.ParentId, i.Name }).IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ParentId);
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.Description).HasMaxLength(256);

                if (options.MapRelationship)
                {
                    b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
                }
            });

            modelBuilder.Entity<Source>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.ParentId, i.Name }).IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ParentId);
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.Description).HasMaxLength(256);
                b.Property(p => p.Website).HasMaxLength(256);
                b.Property(p => p.Weblogo).HasMaxLength(256);

                if (options.MapRelationship)
                {
                    b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.SourceId).IsRequired();
                }
            });

            modelBuilder.Entity<Claim>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();
                b.Property(p => p.Description).HasMaxLength(256);

                if (options.MapRelationship)
                {
                    b.HasMany<PaneClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                    b.HasMany<UnitClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                }
            });

            modelBuilder.Entity<Tag>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => i.Name).IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.Name).HasMaxLength(50).IsRequired();

                if (options.MapRelationship)
                {
                    b.HasMany<UnitTag>().WithOne().HasForeignKey(fk => fk.TagId).IsRequired();
                }
            });

            modelBuilder.Entity<Unit>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.CategoryId, i.Title }).IsUnique();
                b.HasIndex(i => new { i.PublishedBy, i.PublishedTime });

                b.Property(p => p.Title).HasMaxLength(256).IsRequired();
                b.Property(p => p.Subtitle).HasMaxLength(256);
                b.Property(p => p.Reference).HasMaxLength(256);
                b.Property(p => p.PublishedAs).HasMaxLength(256);

                if (options.MapRelationship)
                {
                    b.HasMany<Category>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<Pane>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<Source>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<UnitClaim>().WithOne().HasForeignKey(fk => fk.UnitId).IsRequired();
                    b.HasMany<UnitVisitCount>().WithOne().HasForeignKey(fk => fk.UnitId).IsRequired();
                }
            });

            modelBuilder.Entity<UnitClaim>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UnitId, i.ClaimId });

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ClaimValue).IsRequired(); // 不限长度

                if (options.MapRelationship)
                {
                    b.HasMany<Claim>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            modelBuilder.Entity<UnitTag>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.UnitId, i.TagId });

                b.Property(p => p.Id).ValueGeneratedOnAdd();

                if (options.MapRelationship)
                {
                    b.HasMany<Tag>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            modelBuilder.Entity<UnitVisitCount>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.UnitId);

                b.Property(p => p.SupporterCount);
                b.Property(p => p.ObjectorCount);
                b.Property(p => p.FavoriteCount);
                b.Property(p => p.RetweetCount);

                b.Property(p => p.VisitCount);
                b.Property(p => p.VisitorCount);

                if (options.MapRelationship)
                {
                    b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            modelBuilder.Entity<Pane>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.ParentId, i.Name }).IsUnique();

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ParentId);
                b.Property(p => p.Name).HasMaxLength(256);
                b.Property(p => p.Description).HasMaxLength(256);
                b.Property(p => p.Icon).HasMaxLength(256);
                b.Property(p => p.More).HasMaxLength(256);

                if (options.MapRelationship)
                {
                    b.HasMany<PaneClaim>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
                    b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
                }
            });

            modelBuilder.Entity<PaneClaim>(b =>
            {
                b.ToTableByPluralize();

                b.HasKey(k => k.Id);

                b.HasIndex(i => new { i.PaneId, i.ClaimId });

                b.Property(p => p.Id).ValueGeneratedOnAdd();
                b.Property(p => p.ClaimValue).IsRequired(); // 不限长度

                if (options.MapRelationship)
                {
                    b.HasMany<Claim>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                    b.HasMany<Pane>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                }
            });

            return modelBuilder;
        }

    }
}
