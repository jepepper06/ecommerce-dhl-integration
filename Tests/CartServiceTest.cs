using Model;
using Moq;
using Services.Cart;
using Services.Repositories;
using Xunit;

namespace Tests;

public class CartServiceTest
{
    private Mock<IRepository<Item>> _itemRepositoryMock;
    private Mock<IRepository<Order>> _orderRepositoryMock;
    private Mock<IRepository<Product>> _productRepositoryMock;
    private Mock<IRepository<Customer>> _clientRepositoryMock;
    private Order OrderTestBase { get; set; } = new(){
                Id = 1,
                Total = 2.4,
                Items = new List<Item>(){
                    new(){
                        Id = 1,
                        Product = new(){
                            Id = 1,
                            Price = 2.4
                        },
                        Quantity = 1
                    }
                }                
            };
    private Customer ClientTestBase {get; set;} = new(){
            Id = 1,
            Name = "asdasd",
            Email = "asdasd",
            Document = "V2342342",
            PhoneNumber = "89291328731", 
            Orders = new List<Order>(){
                new(){
                    Id = 1,
                    Total = 0.00
                }
            }
        };
    private ICartService _cartService; 
    public CartServiceTest()
    {
    _itemRepositoryMock = new Mock<IRepository<Item>>();
    _orderRepositoryMock = new Mock<IRepository<Order>>();
    _productRepositoryMock = new Mock<IRepository<Product>>();
    _clientRepositoryMock = new Mock<IRepository<Customer>>();
    _cartService = new CartService(
        _orderRepositoryMock.Object,
        _itemRepositoryMock.Object,
        _productRepositoryMock.Object,
        _clientRepositoryMock.Object);
    _clientRepositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
        .ReturnsAsync((long Id) => ClientTestBase);
    _productRepositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
        .ReturnsAsync((long p) => new(){
            Id = 1,
            Name = "assdas",
            Price = 2.3
        });
    _itemRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Item>()))
        .ReturnsAsync((Item i) => i);
    _orderRepositoryMock.Setup(r => r.GetAsync(It.IsAny<long>()))
        .ReturnsAsync((long id) => {
            return OrderTestBase;
        });
    _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
        .ReturnsAsync((Order o) => o);
    _orderRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Order>()))
        .ReturnsAsync((Order o) => o);
    _orderRepositoryMock.Setup(r => r.SaveChangesAsync());
    
    }
    [Fact]
    public async void AddProductToCart()
    {
        var order = await _cartService.AddProductToCart(1L,1L,2,2L);
        Assert.Equal(4.6,order.Total);
    }
    [Fact]
    public async void RemoveProductFromCart()
    {
        var order = await _cartService.RemoveProductFromCart(1,1);
        Assert.Equal(0,order.Total);
    }
    [Fact]
    public async void GetCartTest(){
        var order = await _cartService.GetCart(1);
        Assert.Equivalent(OrderTestBase, order);
    }
}