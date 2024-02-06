using AutoMapper;
using Contracts;
using Model;

namespace Services.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductCreationDTO, Product>();
        CreateMap<Product, ProductPublicDTO>();
        CreateMap<ProductUpdateDTO, Product>();

    }
}