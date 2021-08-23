#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions.Data.Accessing;
using Microsoft.EntityFrameworkCore;

namespace WebExample
{
    class InternalPortalAccessorMigrator : IAccessorMigrator
    {
        public void Migrate(IReadOnlyList<AccessorDescriptor> descriptors)
        {
            foreach (var descr in descriptors)
            {
                var context = (DbContext)descr.Accessor;
                context.Database.Migrate();
            }
        }

        public async Task MigrateAsync(IReadOnlyList<AccessorDescriptor> descriptors,
            CancellationToken cancellationToken = default)
        {
            foreach (var descr in descriptors)
            {
                var context = (DbContext)descr.Accessor;
                await context.Database.MigrateAsync(cancellationToken);
            }
        }

    }
}
