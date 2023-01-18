using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace CustomerRegister.Register
{
    public class TemplateInfo : AuditedAggregateRoot<Guid>
    {
        public string TemplateData { get; set; }
        public string TemplateName { get; set; }

    }
}
