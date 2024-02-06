using Model;
using Moq;
using Services.Admin;
using Services.Repositories;
using Xunit;
namespace Tests;

public class AdminServiceTest
{
    private List<Customer> ClientsMocks { get; set; }
    private Mock<IRepository<Product>> _productRepositoryMock;
    private Mock<IRepository<Order>> _orderRepositoryMock;
    private Mock<IRepository<Customer>> _clientRepositoryMock;
    private Mock<IRepository<Supplier>> _supplierRepositoryMock;
    private IAdminService _adminService;
    public AdminServiceTest()
    {   
        _productRepositoryMock = new Mock<IRepository<Product>>();
        _orderRepositoryMock = new Mock<IRepository<Order>>();
        _clientRepositoryMock = new Mock<IRepository<Customer>>();
        _supplierRepositoryMock = new Mock<IRepository<Supplier>>();

        _adminService = new AdminService(
            _productRepositoryMock.Object,
            _orderRepositoryMock.Object,
            _clientRepositoryMock.Object,
            _supplierRepositoryMock.Object);
        
        _clientRepositoryMock
            .Setup(r => r.GetAllAsync(It.IsAny<int>()))
            .ReturnsAsync(() => ClientPopulationHelper(new List<Customer>()));
        _clientRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Customer>()))
            .ReturnsAsync((Customer c) => c);
        _clientRepositoryMock
            .Setup(r => r.SaveChangesAsync());
        _productRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product p) => p);
        _productRepositoryMock
            .Setup(r => r.SaveChangesAsync());
        _productRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Product>()))
            .ReturnsAsync((Product p) => p);
        _orderRepositoryMock
            .Setup(r => r.UpdateAsync(It.IsAny<Order>()))
            .ReturnsAsync((Order o) => o);

        ClientsMocks = ClientPopulationHelper(new List<Customer>()).ToList();
    }
    private IEnumerable<Customer> ClientPopulationHelper(List<Customer> clients){
        for(int i = 0; i < 301; i++){
            clients.Add(new(){
                Id = i,
                Name = $"Juan Castillo {i}",
                Email = $"juansebastiancastilloojeda{i}@gmail.com",
                Document = "V28172071",
                PhoneNumber = ""
            });
        }
        return clients;
    }
    [Fact]
    public async void CreateProductTest(){
        var product = new Product(){
            Id = 1,
            Name = "Vino de Uva",
            Price = 250.0,
            Supplier = new(){
                Id = 1,
                Name = "MercadoLibre",
                BaseUrl = "mercadolibre.com",
                Description = ""
                },
            Path = ""
        };
        var testProduct = await _adminService.CreateProduct(product);
        Assert.Equal(product,testProduct);
    } 
    [Fact]
    public async void GetClientsTest()
    {
        var clients = await _adminService.GetCustomers(1);
        Assert.Equivalent(ClientsMocks,clients);
    }
    [Fact]
    public async void UpdateClientTest()
    {
        var client = Customer.Create(
            "juansebastiancastilloojeda@gmail.com",
            "Juan Castillo",
            "V281721071",
            "+5491123952833"
        );
        client.Email= "";
        client.Name = "";
        var _client = await _adminService.UpdateClient(client);
        Assert.Equivalent(client, _client);
    }
    [Fact]
    public async void UpdateOrderTest()
    {
        Order order = new(){
            Id = 1,
            Customer = new(){
                Id = 1,
                Email = "asdas",
                Name = "asdas",
                Document = "VVV",
                PhoneNumber = "ssssss"
            },
            Total = 0.2,
            IsPayed = false,
            TrackingNumber = "sxsssdasd",
        };
        order.Total = 20.0;
        var _order = await _adminService.UpdateOrder(order);
        Assert.Equal(order.Total, _order.Total);
    }
    [Fact]
    public async void UpdateProductTest()
    {
        Product product = new(){
            Id = 1,
            Name = "Vino Pasita"
        };
        product.Name = "";
        var _product = await _adminService.UpdateProduct(product);
        Assert.Equivalent(product,_product);
    }   
}
