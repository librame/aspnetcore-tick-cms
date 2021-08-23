#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Content;
using Librame.Extensions.Content.Accessing;
using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;
using Librame.Extensions.Portal.Storing;

namespace Librame.Extensions.Portal.Accessing
{
    /// <summary>
    /// 定义抽象实现门户 <see cref="IAccessorSeeder"/>。
    /// </summary>
    public abstract class AbstractPortalAccessorSeeder : AbstractContentAccessorSeeder
    {
        private const string GetIntegrationUsersKey = "GetInitialIntegrationUsers";

        private readonly IPasswordHasher _passwordHasher;


        /// <summary>
        /// 构造一个 <see cref="AbstractPortalAccessorSeeder"/>。
        /// </summary>
        /// <param name="idGeneratorFactory">给定的 <see cref="IIdentificationGeneratorFactory"/>。</param>
        /// <param name="options">给定的 <see cref="ContentExtensionOptions"/>。</param>
        /// <param name="portalOptions">给定的 <see cref="PortalExtensionOptions"/>。</param>
        /// <param name="passwordHasher">给定的 <see cref="IPasswordHasher"/>。</param>
        protected AbstractPortalAccessorSeeder(IIdentificationGeneratorFactory idGeneratorFactory,
            ContentExtensionOptions options, PortalExtensionOptions portalOptions, IPasswordHasher passwordHasher)
            : base(idGeneratorFactory, options)
        {
            PortalOptions = portalOptions;

            _passwordHasher = passwordHasher;
        }


        /// <summary>
        /// 门户扩展选项。
        /// </summary>
        protected PortalExtensionOptions PortalOptions { get; init; }


        /// <summary>
        /// 获取集成用户集合。
        /// </summary>
        /// <returns>返回 <see cref="IntegrationUser"/> 数组。</returns>
        public IntegrationUser[] GetIntegrationUsers()
        {
            return (IntegrationUser[])SeedBank.GetOrAdd(GetIntegrationUsersKey, key =>
            {
                return PortalOptions.InitialIntegrationUsers.Select(pair =>
                {
                    var integrationUser = new IntegrationUser();

                    integrationUser.UserName = pair.Key;

                    var password = string.IsNullOrEmpty(pair.Value) ? PortalOptions.InitialPassword : pair.Value;
                    integrationUser.PasswordHash = _passwordHasher.HashPassword(password);

                    integrationUser.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return integrationUser;
                });
            });
        }

        /// <summary>
        /// 异步获取集成用户集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="IntegrationUser"/> 数组的异步操作。</returns>
        public Task<IntegrationUser[]> GetIntegrationUsersAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetIntegrationUsers);


        /// <summary>
        /// 获取集成用户集合。
        /// </summary>
        /// <returns>返回 <see cref="IntegrationUser"/> 数组。</returns>
        public IntegrationUser[] GetIntegrationUsers()
        {
            return (IntegrationUser[])SeedBank.GetOrAdd(GetIntegrationUsersKey, key =>
            {
                return PortalOptions.InitialIntegrationUsers.Select(pair =>
                {
                    var integrationUser = new IntegrationUser();

                    integrationUser.UserName = pair.Key;

                    var password = string.IsNullOrEmpty(pair.Value) ? PortalOptions.InitialPassword : pair.Value;
                    integrationUser.PasswordHash = _passwordHasher.HashPassword(password);

                    integrationUser.PopulateCreation(GetInitialUserId(), Clock.GetUtcNow());

                    return integrationUser;
                });
            });
        }

        /// <summary>
        /// 异步获取集成用户集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个包含 <see cref="IntegrationUser"/> 数组的异步操作。</returns>
        public Task<IntegrationUser[]> GetIntegrationUsersAsync(CancellationToken cancellationToken = default)
            => cancellationToken.RunTask(GetIntegrationUsers);

    }
}
