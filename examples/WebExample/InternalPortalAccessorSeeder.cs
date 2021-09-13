using Librame.Extensions.Data;
using Librame.Extensions.Portal;
using Librame.Extensions.Portal.Accessing;
using Librame.Extensions.Portal.Storing;

namespace WebExample
{
    class InternalPortalAccessorSeeder : AbstractPortalAccessorSeeder<IntegrationUser>
    {
        public InternalPortalAccessorSeeder(IIdentificationGeneratorFactory idGeneratorFactory,
            PortalExtensionOptions portalOptions, IPasswordHasher<IntegrationUser> passwordHasher)
            : base(idGeneratorFactory, portalOptions, passwordHasher)
        {
            PostPopulateUserAction = (user, initialUserId, clock)
                => user.PopulateCreation(initialUserId, clock.GetUtcNow());
        }

    }
}
