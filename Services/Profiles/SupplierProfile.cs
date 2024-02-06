using AutoMapper;
using Contracts;
using Model;

namespace Services.Profiles;

public class SupplierProfile : Profile
{
    public SupplierProfile()
    {
        CreateMap<SupplierCreationDTO,Supplier>();
    }
}