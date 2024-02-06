using System.Linq;
using System.Linq.Expressions;
using Model;
using Moq;
using Services.Repositories;
using Services.Shopping;
using Xunit;

namespace Tests;

public class ShoppingServiceTest
{
    private Mock<IRepository<Product>> _productRepositoryMock{ get; set; }
    private IShoppingService _shoppingService { get; set; }
    private List<Product> ProductsTestBase = new List<Product>();
    private List<Product> ProductTestBaseInitializer()
    {
        List<Product> products = new();
        for(int i = 0; i < 501; i++){
            products.Add(new(){
                Id = i,
                Name = $"Product {i}",
                Price = i + 0.2
            });
        }
        return products;
    }
    public ShoppingServiceTest(){
        ProductsTestBase = ProductTestBaseInitializer();
        _productRepositoryMock = new Mock<IRepository<Product>>();
        _shoppingService = new ShoppingService(_productRepositoryMock.Object);
        _productRepositoryMock
            .Setup(r => r.FindAsync(It.IsAny<Expression<Func<Product,bool>>>()))
            .ReturnsAsync((Expression<Func<Product, bool>> predicate) => { 
                return ProductsTestBase.AsQueryable().Where(predicate.Compile()).ToList();
            });
        _productRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<int>()))
            .ReturnsAsync((int page) => ProductsTestBase
                .AsQueryable()
                .Skip((page - 1) * 100)
                .Take(100)
                .ToList());
    }
    [Fact]
    public async void GetProductByNameTest()
    {
        var productList = await _shoppingService.GetProductByName("Product 1");
        var expectedProduct = new Product(){
            Id = 1,
            Name = "Product 1",
            Price = 1.2
        };
        Assert.Equivalent(expectedProduct,productList.First());
    }
    [Fact]
    public async void GetProductsPaginated()
    {
        var productsFirst100 = await _shoppingService.GetProductsPaginated(1);
        var ProductTestBaseChunkOf100 = ProductsTestBase.Chunk(100);
        var ProductsTestBaseFirstChunk = ProductTestBaseChunkOf100.First().ToList();
        Console.WriteLine(ProductsTestBaseFirstChunk);
        Assert.Equivalent(ProductsTestBaseFirstChunk,productsFirst100);

    }
}