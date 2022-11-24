using CustomerRegister.Dtos;
using CustomerRegister.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CustomerRegister.CustomerRegister
{
    public interface ICustomerInfoServices : IApplicationService
    {
        Task<PagedResultDto<CustomerInfoDto>> GetCustomers(GetCustomerDto input);
        Task<CustomerInfoDto> CreateAsync(CreateUpdateCustomerDto input);
        //bool SendEmail(EmailData emailData);
    }
}
