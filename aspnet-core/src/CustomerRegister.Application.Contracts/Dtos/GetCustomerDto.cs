using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace CustomerRegister.Dtos
{
    public class GetCustomerDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
