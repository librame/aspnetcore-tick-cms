namespace WebExample;

class InternalPortalAccessorSeeder : AbstractPortalAccessorSeeder<IntegrationUser>
{
    public InternalPortalAccessorSeeder(IPasswordHasher<IntegrationUser> passwordHasher,
        PortalExtensionOptions portalOptions, IIdentificationGeneratorFactory idGeneratorFactory)
        : base(passwordHasher, portalOptions, idGeneratorFactory)
    {
        PostPopulateUserAction = (user, initialUserId, clock)
            => user.PopulateCreation(initialUserId, clock.GetUtcNow());
    }

}
