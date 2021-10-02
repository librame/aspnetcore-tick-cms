#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content.Accessing;
using Librame.Extensions.Core;
using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;
using Librame.Extensions.Portal.Storing;

namespace Librame.Extensions.Portal.Accessing;

/// <summary>
/// 定义抽象实现 <see cref="IAccessorSeeder"/> 的门户访问器种子机。
/// </summary>
/// <typeparam name="TUser">指定实现 <see cref="IUser"/> 的用户类型。</typeparam>
public abstract class AbstractPortalAccessorSeeder<TUser> : AbstractContentAccessorSeeder
    where TUser : class, IUser
{
    private readonly IPasswordHasher<TUser> _passwordHasher;
    private string? _initialUserId;


    /// <summary>
    /// 构造一个 <see cref="AbstractPortalAccessorSeeder{TUser}"/>。
    /// </summary>
    /// <param name="passwordHasher">给定的 <see cref="IPasswordHasher{TUser}"/>。</param>
    /// <param name="portalOptions">给定的 <see cref="PortalExtensionOptions"/>。</param>
    /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
    protected AbstractPortalAccessorSeeder(IPasswordHasher<TUser> passwordHasher,
        PortalExtensionOptions portalOptions, IIdentificationGeneratorFactory idGeneratorFactory)
        : base(portalOptions.ContentOptions, idGeneratorFactory)
    {
        PortalOptions = portalOptions;

        _passwordHasher = passwordHasher;
    }


    /// <summary>
    /// 门户扩展选项。
    /// </summary>
    protected PortalExtensionOptions PortalOptions { get; init; }

    /// <summary>
    /// 后置填充用户动作。
    /// </summary>
    public Action<TUser, string, IClock>? PostPopulateUserAction { get; init; }


    /// <summary>
    /// 获取初始用户标识。
    /// </summary>
    /// <returns>返回标识字符串。</returns>
    public override string GetInitialUserId()
    {
        if (string.IsNullOrEmpty(_initialUserId))
            _initialUserId = IdGeneratorFactory.GetNewId<string>();

        return _initialUserId;
    }


    /// <summary>
    /// 获取用户集合。
    /// </summary>
    /// <returns>返回 <see cref="IEnumerable{TUser}"/> 数组。</returns>
    public IEnumerable<TUser> GetUsers()
    {
        return Seed(nameof(GetUsers), key =>
        {
            return PortalOptions.InitialUsers.Select((pair, i) =>
            {
                var user = ExpressionExtensions.New<TUser>();

                user.Id = i is 0 ? GetInitialUserId()! : IdGeneratorFactory.GetNewId<string>();
                user.UserName = pair.Key;

                var password = string.IsNullOrEmpty(pair.Value) ? PortalOptions.InitialPassword : pair.Value;
                user.PasswordHash = _passwordHasher.HashPassword(user, password);

                PostPopulateUserAction?.Invoke(user, GetInitialUserId()!, Clock);

                return user;
            });
        });
    }

    /// <summary>
    /// 异步获取用户集合。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
    /// <returns>返回一个包含 <see cref="IEnumerable{IntegrationUser}"/> 数组的异步操作。</returns>
    public Task<IEnumerable<TUser>> GetUsersAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunTask(GetUsers);


    /// <summary>
    /// 获取编者集合。
    /// </summary>
    /// <returns>返回 <see cref="IEnumerable{Editor}"/> 数组。</returns>
    public IEnumerable<Editor> GetEditors()
    {
        return Seed(nameof(GetEditors), key =>
        {
            var users = GetUsers();

            return PortalOptions.InitialEditors.Select((pair, i) =>
            {
                var editor = new Editor();

                editor.Id = IdGeneratorFactory.GetNewId<string>();
                editor.Name = pair.Key;
                editor.Description = pair.Value.Description;
                editor.Portrait = pair.Value.Portrait;
                editor.UserId = users.First(p => p.UserName == pair.Value.UserName).Id;

                editor.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                return editor;
            });
        });
    }

    /// <summary>
    /// 异步获取编者集合。
    /// </summary>
    /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
    /// <returns>返回一个包含 <see cref="IEnumerable{Editor}"/> 数组的异步操作。</returns>
    public Task<IEnumerable<Editor>> GetEditorsAsync(CancellationToken cancellationToken = default)
        => cancellationToken.RunTask(GetEditors);

}
