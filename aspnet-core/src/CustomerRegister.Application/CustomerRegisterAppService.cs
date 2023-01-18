using CustomerRegister.Localization;
using Volo.Abp.Application.Services;

namespace CustomerRegister;

/* Inherit your application services from this class.
 */
public abstract class CustomerRegisterAppService : ApplicationService
{
    protected CustomerRegisterAppService()
    {
        LocalizationResource = typeof(CustomerRegisterResource);
    }
}
