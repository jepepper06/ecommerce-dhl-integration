using AutoMapper;
using Contracts;
using Model;

namespace Services.Profiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<CustomerRegistrationDTO,Customer>()
            .ForMember(dest => dest.Orders,(opt) => opt.MapFrom(src => new List<Order>()));
    }
}