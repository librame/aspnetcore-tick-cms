var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var modelAssemblyName = typeof(IntegrationUser).Assembly.FullName;

// 配置 MySQL、SQLServer、SQLite 三种数据库的 RAHID 功能（默认使用集成用户）
//builder.Services.AddDbContext<MySqlPortalAccessor<IntegrationUser>>(opts =>
//{
//    opts.UseMySql(MySqlConnectionStringHelper.Validate(builder.Configuration.GetConnectionString("MySqlConnectionString"), out var version), version,
//        a => a.MigrationsAssembly(modelAssemblyName).MigrationsAssembly(modelAssemblyName));

//    opts.UseAccessor(b => b.WithAccess(AccessMode.Write));
//});

builder.Services.AddDbContextPool<SqlServerPortalAccessor<IntegrationUser>>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString"),
        a => a.MigrationsAssembly(modelAssemblyName).MigrationsAssembly(modelAssemblyName));

    opts.UseAccessor(b => b.WithAccess(AccessMode.ReadWrite).WithPooling());
});

builder.Services.AddDbContext<SqlitePortalAccessor<IntegrationUser>>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString"),
        a => a.MigrationsAssembly(modelAssemblyName).MigrationsAssembly(modelAssemblyName));

    opts.UseAccessor(b => b.WithAccess(AccessMode.Write).WithPriority(2f));
});

builder.Services.AddLibrame()
    .AddData(opts =>
    {
        // 测试时每次运行需新建数据库
        opts.Access.EnsureDatabaseDeleted = false;

        // 每次修改选项时自动保存为 JSON 文件
        opts.PropertyChangedAction = (o, e) => o.SaveOptionsAsJson();
    })
    .AddSeeder<InternalPortalAccessorSeeder>()
    //.AddInitializer<InternalPortalAccessorInitializer<MySqlPortalAccessor<IntegrationUser>, InternalPortalAccessorSeeder, IntegrationUser>>()
    .AddInitializer<InternalPortalAccessorInitializer<SqlServerPortalAccessor<IntegrationUser>, InternalPortalAccessorSeeder, IntegrationUser>>()
    .AddInitializer<InternalPortalAccessorInitializer<SqlitePortalAccessor<IntegrationUser>, InternalPortalAccessorSeeder, IntegrationUser>>()
    .AddContent()
    .AddPortal()
    .SaveOptionsAsJson(); // 首次保存选项为 JSON 文件

var app = builder.Build();

using (var score = app.Services.CreateScope())
{
    score.ServiceProvider.UseAccessorInitializer();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
