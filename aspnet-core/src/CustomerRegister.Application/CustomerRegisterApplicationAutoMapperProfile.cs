using AutoMapper;
using CustomerRegister.Dtos;
using CustomerRegister.Register;

namespace CustomerRegister;

public class CustomerRegisterApplicationAutoMapperProfile : Profile
{
    public CustomerRegisterApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<CustomerInfo, CustomerInfoDto>();
        CreateMap<CreateUpdateCustomerDto, CustomerInfo>();
    }
}
