using CustomerRegister.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CustomerRegister.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CustomerRegisterEntityFrameworkCoreModule),
    typeof(CustomerRegisterApplicationContractsModule)
    )]
public class CustomerRegisterDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
