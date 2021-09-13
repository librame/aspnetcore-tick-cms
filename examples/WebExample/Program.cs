using Librame.Extensions.Content.Storing;
using Librame.Extensions.Core;
using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;
using Librame.Extensions.Data.Storing;
using Librame.Extensions.Portal.Accessing;
using Librame.Extensions.Portal.Storing;
using Microsoft.EntityFrameworkCore;
using WebExample;

var builder = WebApplication.CreateBuilder(args);

// 配置 MySQL、SQLServer、SQLite 三种数据库的 RAHID 功能（默认使用集成用户）
builder.Services.AddDbContext<MySqlPortalAccessor<IntegrationUser>>(opts =>
{
    opts.UseMySql(MySqlConnectionStringHelper.Validate(builder.Configuration.GetConnectionString("MySqlConnString"), out var version), version,
        a => a.MigrationsAssembly(typeof(IntegrationUser).Assembly.FullName).MigrationsAssembly(typeof(Unit).Assembly.FullName));

    opts.UseAccessor(b => b.WithInteraction(AccessorInteraction.Write).WithPriority(1));
});

builder.Services.AddDbContextPool<SqlServerPortalAccessor<IntegrationUser>>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnString"),
        a => a.MigrationsAssembly(typeof(IntegrationUser).Assembly.FullName).MigrationsAssembly(typeof(Unit).Assembly.FullName));

    opts.UseAccessor(b => b.WithInteraction(AccessorInteraction.Write).WithPooling().WithPriority(2));
});

builder.Services.AddDbContext<SqlitePortalAccessor<IntegrationUser>>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnString"),
        a => a.MigrationsAssembly(typeof(IntegrationUser).Assembly.FullName).MigrationsAssembly(typeof(Unit).Assembly.FullName));

    opts.UseAccessor(b => b.WithInteraction(AccessorInteraction.Read));
});

builder.Services.AddLibrame()
    .AddData(opts =>
    {
        // 测试时每次运行需新建数据库
        opts.Access.EnsureDatabaseDeleted = true;

        // 每次修改选项时自动保存为 JSON 文件
        opts.PropertyChangedAction = (o, e) => o.SaveOptionsAsJson();
    })
    .AddSeeder<InternalPortalAccessorSeeder>()
    .AddMigrator<InternalPortalAccessorMigrator>()
    .AddInitializer<InternalPortalAccessorInitializer<MySqlPortalAccessor<IntegrationUser>>>()
    .AddInitializer<InternalPortalAccessorInitializer<SqlServerPortalAccessor<IntegrationUser>>>()
    .AddInitializer<InternalPortalAccessorInitializer<SqlitePortalAccessor<IntegrationUser>>>()
    .AddContent()
    .AddPortal()
    .SaveOptionsAsJson(); // 首次保存选项为 JSON 文件

IList<Unit> units;
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.UseAccessorInitializer();

    var store = scope.ServiceProvider.GetRequiredService<IStore<Unit>>();
    units = store.FindList();
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => units.Select(s => s.ToString()));

app.Run();
