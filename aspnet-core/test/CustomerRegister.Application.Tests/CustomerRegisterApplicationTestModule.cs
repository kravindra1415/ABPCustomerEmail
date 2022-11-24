using Volo.Abp.Modularity;

namespace CustomerRegister;

[DependsOn(
    typeof(CustomerRegisterApplicationModule),
    typeof(CustomerRegisterDomainTestModule)
    )]
public class CustomerRegisterApplicationTestModule : AbpModule
{

}
