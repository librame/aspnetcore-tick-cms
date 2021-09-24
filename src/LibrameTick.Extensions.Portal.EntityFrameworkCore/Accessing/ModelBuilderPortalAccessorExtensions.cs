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

namespace Librame.Extensions.Portal.Accessing;

/// <summary>
/// <see cref="ModelBuilder"/> 门户访问器静态扩展。
/// </summary>
public static class ModelBuilderPortalAccessorExtensions
{

    /// <summary>
    /// 创建门户模型。
    /// </summary>
    /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
    /// <param name="portalAccessor">给定的 <see cref="IPortalAccessor{TUser}"/>。</param>
    /// <returns>返回 <see cref="ModelBuilder"/>。</returns>
    public static ModelBuilder CreatePortalModel<TUser>(this ModelBuilder modelBuilder,
        IPortalAccessor<TUser> portalAccessor)
        where TUser : class, IUser
    {
        var limitableMaxLength = portalAccessor.DataOptions.Store.LimitableMaxLengthOfProperty;

        modelBuilder.Entity<Editor>(b =>
        {
            b.ToTableWithSharding(portalAccessor.ShardingManager);

            b.HasKey(k => k.Id);

            b.HasIndex(i => new { i.UserId, i.Name }).IsUnique();

            b.Property(p => p.Id).ValueGeneratedNever();
            b.Property(p => p.Name).HasMaxLength(100);

            if (limitableMaxLength > 0)
            {
                b.Property(p => p.Description).HasMaxLength(limitableMaxLength);
                b.Property(p => p.Portrait).HasMaxLength(limitableMaxLength);
            }
        });

        modelBuilder.Entity<TUser>(b =>
        {
            b.ToTableWithSharding(portalAccessor.ShardingManager);

            b.HasKey(k => k.Id);

            b.HasIndex(i => i.UserName).IsUnique();

            b.Property(p => p.Id).ValueGeneratedNever();
            b.Property(p => p.UserName).HasMaxLength(100);

            if (limitableMaxLength > 0)
                b.Property(p => p.PasswordHash).HasMaxLength(limitableMaxLength);
        });

        return modelBuilder;
    }

}
