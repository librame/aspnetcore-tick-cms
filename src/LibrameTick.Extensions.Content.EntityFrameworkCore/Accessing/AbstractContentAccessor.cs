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
using Librame.Extensions.Data.Accessing;
using Librame.Extensions.Data.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Librame.Extensions.Content.Accessing
{
    /// <summary>
    /// 定义抽象实现 <see cref="IContentAccessor"/> 的内容访问器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的内容访问器类型。</typeparam>
    public abstract class AbstractContentAccessor<TAccessor> : AbstractAccessor<TAccessor>, IContentAccessor
        where TAccessor : AbstractAccessor, IContentAccessor
    {

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

        /// <summary>
        /// 构造一个 <see cref="AbstractContentAccessor{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="DbContextOptions{TAccessor}"/>。</param>
        protected AbstractContentAccessor(DbContextOptions<TAccessor> options)
            : base(options)
        {
        }


        /// <summary>
        /// 类别数据集。
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// 声明数据集。
        /// </summary>
        public DbSet<Claim> Claims { get; set; }

        /// <summary>
        /// 窗格数据集。
        /// </summary>
        public DbSet<Pane> Panes { get; set; }

        /// <summary>
        /// 窗格单元数据集。
        /// </summary>
        public DbSet<PaneClaim> PaneClaims { get; set; }

        /// <summary>
        /// 窗格单元数据集。
        /// </summary>
        public DbSet<PaneUnit> PaneUnits { get; set; }

        /// <summary>
        /// 来源数据集。
        /// </summary>
        public DbSet<Source> Sources { get; set; }

        /// <summary>
        /// 标签数据集。
        /// </summary>
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// 单元数据集。
        /// </summary>
        public DbSet<Unit> Units { get; set; }

        /// <summary>
        /// 单元声明数据集。
        /// </summary>
        public DbSet<UnitClaim> UnitClaims { get; set; }

        /// <summary>
        /// 单元标签数据集。
        /// </summary>
        public DbSet<UnitTag> UnitTags { get; set; }

        /// <summary>
        /// 单元统计数据集。
        /// </summary>
        public DbSet<UnitVisitCount> UnitVisitCounts { get; set; }

#pragma warning restore CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。


        /// <summary>
        /// 开始模型创建。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var provider = ((IInfrastructure<IServiceProvider>)this).Instance;

            var options = this.GetService<ContentExtensionOptions>();
            modelBuilder.CreateContentModel(options);

            AppendModelCreating(modelBuilder);

            //var internalServiceProvider = ((IInfrastructure<IServiceProvider>)this).Instance;

            //var service = internalServiceProvider.GetService(typeof(TService))
            //    ?? internalServiceProvider.GetService<IDbContextOptions>()
            //        ?.Extensions.OfType<CoreOptionsExtension>().FirstOrDefault()
            //        ?.ApplicationServiceProvider
            //        ?.GetService(typeof(TService));

            var coreOptions = _options.Extensions.OfType<CoreOptionsExtension>().FirstOrDefault();
            var p = coreOptions?.ApplicationServiceProvider;

            var converterFactory = coreOptions?.InternalServiceProvider?.GetService<IEncryptionConverterFactory>();
            var f = p!.GetService<IEncryptionConverterFactory>();
            modelBuilder.UseEncryption(converterFactory, AccessorType);
        }

        /// <summary>
        /// 附加模型创建。
        /// </summary>
        /// <param name="modelBuilder">给定的 <see cref="ModelBuilder"/>。</param>
        protected virtual void AppendModelCreating(ModelBuilder modelBuilder)
        {
        }

    }
}
