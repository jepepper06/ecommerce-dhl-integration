using AutoMapper;
using Contracts;
using Model;
using Services.Repositories;

namespace Services.Shopping;

public interface IShoppingService
{
    Task<IEnumerable<ProductPublicDTO>> GetProductsPaginated(int page);
    Task<IEnumerable<ProductPublicDTO>> GetProductByName(string name); 
}

public class ShoppingService : IShoppingService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;
    public ShoppingService(
        IRepository<Product> productRepository,
        IMapper mapper){
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProductPublicDTO>> GetProductByName(string name)
    {
        return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductPublicDTO>>(await _productRepository
            .FindAsync(p => p.Name.ToLower().Contains(name.ToLower())));
    }

    public async Task<IEnumerable<ProductPublicDTO>> GetProductsPaginated(int page)
    {
        return _mapper.Map<IEnumerable<Product>,IEnumerable<ProductPublicDTO>>(await _productRepository
            .GetAllAsync(page)); 
    }
}