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

namespace Librame.Extensions.Content.Accessing;

/// <summary>
/// <see cref="ModelBuilder"/> 内容访问器静态扩展。
/// </summary>
public static class ModelBuilderContentAccessorExtensions
{

    /// <summary>
    /// 创建内容模型。
    /// </summary>
    /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
    /// <param name="contentAccessor">给定的 <see cref="IContentAccessor"/>。</param>
    /// <returns>返回 <see cref="ModelBuilder"/>。</returns>
    public static ModelBuilder CreateContentModel(this ModelBuilder modelBuilder,
        IContentAccessor contentAccessor)
    {
        var limitableMaxLength = contentAccessor.DataOptions.Store.LimitableMaxLengthOfProperty;
        var mapRelationship = contentAccessor.DataOptions.Store.MapRelationship;

        modelBuilder.Entity<Category>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.ParentId, i.Name }).IsUnique();

            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(p => p.ParentId);
            b.Property(p => p.Name).HasMaxLength(50).IsRequired();

            if (limitableMaxLength > 0)
                b.Property(p => p.Description).HasMaxLength(limitableMaxLength);

            if (mapRelationship)
            {
                b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.CategoryId).IsRequired();
            }
        });

        modelBuilder.Entity<Source>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.ParentId, i.Name }).IsUnique();

            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(p => p.ParentId);
            b.Property(p => p.Name).HasMaxLength(50).IsRequired();

            if (limitableMaxLength > 0)
            {
                b.Property(p => p.Description).HasMaxLength(limitableMaxLength);
                b.Property(p => p.Website).HasMaxLength(limitableMaxLength);
                b.Property(p => p.Weblogo).HasMaxLength(limitableMaxLength);
            }

            if (mapRelationship)
            {
                b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.SourceId).IsRequired();
            }
        });

        modelBuilder.Entity<Claim>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => i.Name).IsUnique();

            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(p => p.Name).HasMaxLength(50).IsRequired();

            if (limitableMaxLength > 0)
                b.Property(p => p.Description).HasMaxLength(limitableMaxLength);

            if (mapRelationship)
            {
                b.HasMany<PaneClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
                b.HasMany<UnitClaim>().WithOne().HasForeignKey(fk => fk.ClaimId).IsRequired();
            }
        });

        modelBuilder.Entity<Pane>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.ParentId, i.Name }).IsUnique();

            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(p => p.ParentId);
            b.Property(p => p.Name).HasMaxLength(50);

            if (limitableMaxLength > 0)
            {
                b.Property(p => p.Description).HasMaxLength(limitableMaxLength);
                b.Property(p => p.Icon).HasMaxLength(limitableMaxLength);
                b.Property(p => p.More).HasMaxLength(limitableMaxLength);
            }

            if (mapRelationship)
            {
                b.HasMany<PaneClaim>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
                b.HasMany<PaneUnit>().WithOne().HasForeignKey(fk => fk.PaneId).IsRequired();
            }
        });

        modelBuilder.Entity<PaneClaim>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.PaneId, i.ClaimId });

            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(p => p.ClaimValue).IsRequired(); // 不限长度

            if (mapRelationship)
            {
                b.HasMany<Claim>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                b.HasMany<Pane>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
            }
        });

        modelBuilder.Entity<PaneUnit>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => i.PaneId);

            b.Property(p => p.Id).ValueGeneratedOnAdd();

            if (mapRelationship)
            {
                b.HasMany<Pane>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
            }
        });

        modelBuilder.Entity<Tag>(b =>
        {
            b.ToTable();

            b.HasKey(k => k.Id);

            b.HasIndex(i => i.Name).IsUnique();

            b.Property(p => p.Id).ValueGeneratedOnAdd();
            b.Property(p => p.Name).HasMaxLength(50).IsRequired();

            if (mapRelationship)
            {
                b.HasMany<UnitTag>().WithOne().HasForeignKey(fk => fk.TagId).IsRequired();
            }
        });

        modelBuilder.Entity<Unit>(b =>
        {
            b.ToTableWithSharding(contentAccessor.ShardingManager);

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.CategoryId, i.Title }).IsUnique();
            b.HasIndex(i => new { i.PublishedBy, i.PublishedTime });

            b.Property(p => p.Id).ValueGeneratedNever();

            if (limitableMaxLength > 0)
            {
                b.Property(p => p.Title).HasMaxLength(limitableMaxLength).IsRequired();
                b.Property(p => p.Subtitle).HasMaxLength(limitableMaxLength);
                b.Property(p => p.Reference).HasMaxLength(limitableMaxLength);
                b.Property(p => p.PublishedAs).HasMaxLength(limitableMaxLength);
                b.Property(p => p.Cover).HasMaxLength(limitableMaxLength);
            }

            b.Property(p => p.Body);

            if (mapRelationship)
            {
                b.HasMany<Category>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                b.HasMany<Source>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                b.HasMany<UnitClaim>().WithOne().HasForeignKey(fk => fk.UnitId).IsRequired();
                b.HasMany<UnitVisitCount>().WithOne().HasForeignKey(fk => fk.UnitId).IsRequired();
            }
        });

        modelBuilder.Entity<UnitClaim>(b =>
        {
            b.ToTableWithSharding(contentAccessor.ShardingManager);

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.UnitId, i.ClaimId });

            b.Property(p => p.Id).ValueGeneratedNever();
            b.Property(p => p.ClaimValue).IsRequired(); // 不限长度

            if (mapRelationship)
            {
                b.HasMany<Claim>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
            }
        });

        modelBuilder.Entity<UnitTag>(b =>
        {
            b.ToTableWithSharding(contentAccessor.ShardingManager);

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.UnitId, i.TagId });

            b.Property(p => p.Id).ValueGeneratedNever();

            if (mapRelationship)
            {
                b.HasMany<Tag>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
                b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
            }
        });

        modelBuilder.Entity<UnitVisitCount>(b =>
        {
            b.ToTableWithSharding(contentAccessor.ShardingManager);

            b.HasKey(k => k.UnitId);

            b.Property(p => p.UnitId).ValueGeneratedNever();

            b.Property(p => p.SupporterCount);
            b.Property(p => p.ObjectorCount);
            b.Property(p => p.FavoriteCount);
            b.Property(p => p.RetweetCount);

            b.Property(p => p.VisitCount);
            b.Property(p => p.VisitorCount);

            if (mapRelationship)
            {
                b.HasMany<Unit>().WithOne().HasForeignKey(fk => fk.Id).IsRequired();
            }
        });

        return modelBuilder;
    }

}
