using Librame.Extensions.Data;
using Librame.Extensions.Data.Accessing;
using Librame.Extensions.Portal.Accessing;
using Librame.Extensions.Portal.Storing;

namespace WebExample
{
    class InternalPortalAccessorInitializer<TAccessor> : AbstractAccessorInitializer<TAccessor>
        where TAccessor : AbstractAccessor, IPortalAccessor<IntegrationUser>
    {
        private InternalPortalAccessorSeeder _seeder;


        public InternalPortalAccessorInitializer(TAccessor accessor, InternalPortalAccessorSeeder seeder)
            : base(accessor)
        {
            _seeder = seeder;
        }


        protected override void Populate(IServiceProvider services, DataExtensionOptions options)
        {
            if (!Accessor.Users.LocalOrDbAny())
            {
                var users = _seeder.GetUsers();

                Accessor.Users.AddRange(users);

                Accessor.SaveChanges();
            }
        }

        protected override async Task PopulateAsync(IServiceProvider services, DataExtensionOptions options,
            CancellationToken cancellationToken = default)
        {
            if (!await Accessor.Users.LocalOrDbAnyAsync(cancellationToken: cancellationToken))
            {
                var users = await _seeder.GetUsersAsync(cancellationToken);

                await Accessor.Users.AddRangeAsync(users, cancellationToken);

                await Accessor.SaveChangesAsync();
            }
        }

    }
}
