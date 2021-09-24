namespace WebExample;

class InternalPortalAccessorInitializer<TAccessor, TSeeder, TUser> : AbstractPortalAccessorInitializer<TAccessor, TSeeder, TUser>
    where TAccessor : AbstractAccessor, IPortalAccessor<TUser>
    where TSeeder : AbstractPortalAccessorSeeder<TUser>
    where TUser : class, IUser
{
    public InternalPortalAccessorInitializer(TAccessor accessor, TSeeder seeder)
        : base(accessor, seeder)
    {
    }

}
