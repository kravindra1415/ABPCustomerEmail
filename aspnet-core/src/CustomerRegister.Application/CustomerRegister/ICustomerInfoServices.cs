using CustomerRegister.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CustomerRegister.CustomerRegister
{
    public interface ICustomerInfoServices : IApplicationService
    {
        Task<PagedResultDto<CustomerInfoDto>> GetCustomers(GetCustomerDto input);
        Task<CustomerInfoDto> CreateAsync(CreateUpdateCustomerDto input);
        Task SendEmailAll(SendAllEmailDto input,string EmailTemplateName);
        Task<List<string>> GetAllTemplates();
        Task<string> GetEmailTemplate(string emailTemplateName);
        //bool SendEmail(EmailData emailData);
    }
}
