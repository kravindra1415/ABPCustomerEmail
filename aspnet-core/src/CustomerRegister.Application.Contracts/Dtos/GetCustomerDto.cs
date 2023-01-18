using Volo.Abp.Application.Dtos;

namespace CustomerRegister.Dtos
{
    public class GetCustomerDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
