namespace CustomerRegister.EmailTemplates
{
    public class EmailTemplateConst
    {
        public const string Registration = "OnRegistration";
        public const string Confirmation = "Confirmation";
    }

    public enum EmailTemplateTypes
    {
        Registration = 1,
        Confirmation = 2
    }

}
