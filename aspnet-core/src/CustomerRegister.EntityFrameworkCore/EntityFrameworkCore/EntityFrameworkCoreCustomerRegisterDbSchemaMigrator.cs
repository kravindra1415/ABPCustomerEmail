using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CustomerRegister.Data;
using Volo.Abp.DependencyInjection;

namespace CustomerRegister.EntityFrameworkCore;

public class EntityFrameworkCoreCustomerRegisterDbSchemaMigrator
    : ICustomerRegisterDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreCustomerRegisterDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the CustomerRegisterDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<CustomerRegisterDbContext>()
            .Database
            .MigrateAsync();
    }
}
