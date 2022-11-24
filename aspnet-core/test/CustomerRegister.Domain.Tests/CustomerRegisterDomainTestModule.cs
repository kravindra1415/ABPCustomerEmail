using CustomerRegister.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CustomerRegister;

[DependsOn(
    typeof(CustomerRegisterEntityFrameworkCoreTestModule)
    )]
public class CustomerRegisterDomainTestModule : AbpModule
{

}
