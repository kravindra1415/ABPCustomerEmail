using CustomerRegister.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CustomerRegister.Permissions;

public class CustomerRegisterPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CustomerRegisterPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CustomerRegisterPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CustomerRegisterResource>(name);
    }
}
