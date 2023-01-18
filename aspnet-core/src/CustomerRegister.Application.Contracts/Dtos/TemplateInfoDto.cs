using System;
using Volo.Abp.Application.Dtos;

namespace CustomerRegister.Dtos
{
    public class TemplateInfoDto : AuditedEntityDto<Guid>
    {
        public string TemplateData { get; set; }
        public string TemplateName { get; set; }
    }
}
