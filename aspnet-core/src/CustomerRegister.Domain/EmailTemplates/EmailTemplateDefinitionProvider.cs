using CustomerRegister.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Localization;
using Volo.Abp.TextTemplating;

namespace CustomerRegister.EmailTemplates
{
    public class EmailTemplateDefinitionProvider : TemplateDefinitionProvider, ITransientDependency
    {
        public override void Define(ITemplateDefinitionContext context)
        {
            context.Add(
                new TemplateDefinition(EmailTemplateConst.Registration,
                displayName: LocalizableString.Create<CustomerRegisterResource>($"TextTemplate:{EmailTemplateConst.Registration}"),
                layout: StandardEmailTemplates.Layout,
                localizationResource: typeof(CustomerRegisterResource)
                ).WithVirtualFilePath($"/EmailTemplates/Templates/Registration.tpl", isInlineLocalized: true));

            context.Add(
                new TemplateDefinition(EmailTemplateConst.Confirmation,
                displayName: LocalizableString.Create<CustomerRegisterResource>($"TextTemplate:{EmailTemplateConst.Confirmation}"),
                layout: StandardEmailTemplates.Layout,
                localizationResource: typeof(CustomerRegisterResource)
                ).WithVirtualFilePath($"/EmailTemplates/Templates/Confirmation.tpl", isInlineLocalized: true));
        }
    }
}
