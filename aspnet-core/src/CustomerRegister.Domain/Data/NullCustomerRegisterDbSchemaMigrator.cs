using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CustomerRegister.Data;

/* This is used if database provider does't define
 * ICustomerRegisterDbSchemaMigrator implementation.
 */
public class NullCustomerRegisterDbSchemaMigrator : ICustomerRegisterDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
