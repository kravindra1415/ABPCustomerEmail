using CustomerRegister.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CustomerRegister.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CustomerRegisterController : AbpControllerBase
{
    protected CustomerRegisterController()
    {
        LocalizationResource = typeof(CustomerRegisterResource);
    }
}
