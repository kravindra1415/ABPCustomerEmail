using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CustomerRegister;

[Dependency(ReplaceServices = true)]
public class CustomerRegisterBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "CustomerRegister";
}
