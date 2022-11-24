using Volo.Abp.Settings;

namespace CustomerRegister.Settings;

public class CustomerRegisterSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CustomerRegisterSettings.MySetting1));
    }
}
