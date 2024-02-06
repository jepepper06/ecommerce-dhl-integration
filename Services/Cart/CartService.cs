using Model;
using Services.Repositories;

namespace Services.Cart;


public interface ICartService
{
    Task<Order> AddProductToCart(long productId, uint quantity, long clientId);
    Task<Order> RemoveProductFromCart(long itemId, long orderId);
    Task<Order> GetCart(long orderId);
}

public class CartService : ICartService
{
    private readonly IRepository<Item> _itemRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Customer> _customerRepository;
    public CartService(
        IRepository<Order> orderRepository,
        IRepository<Item> itemRepository,
        IRepository<Product> productRepository,
        IRepository<Customer> customerRepository)
    {
        _itemRepository = itemRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;    
    }
    public async Task<Order> AddProductToCart(long productId, uint quantity, long clientId)
    {
        var order = await GetOrCreateOrderByClientId(clientId);
        var product = await _productRepository.GetAsync(productId);
        await AddOrUpdateItem(product,order,quantity);
        order.Total += product.Price * quantity;

        order = await AddOrUpdateOrder(order);
        await _orderRepository.SaveChangesAsync();
        return order;
    }
    private async Task AddOrUpdateItem(Product product, Order order, uint quantity)
    {
        var newItemComparisonObject = order.Items.SingleOrDefault(i => i.Product!.Id == product.Id);
        if(newItemComparisonObject == null)
        {
            var item = Item.Create(product, quantity);
            order.Items.Add(item);
        }else{
            newItemComparisonObject.Quantity += quantity;
            _ = await _itemRepository.UpdateAsync(newItemComparisonObject);
        }
    }
    private async Task<Order> GetOrCreateOrderByClientId(long clientId)
    {
        var client = await _customerRepository.GetAsync(clientId);
        var order = client.Orders.SingleOrDefault(o => o.IsPayed == false);
        if(order == null){
            order = Order.Create();
            order.Customer = client;
        }
        return order;
    }
    private async Task<Order> AddOrUpdateOrder(Order order)
    {
        if(order.Items.Count() == 1)
            return await _orderRepository.AddAsync(order);
        return await _orderRepository.UpdateAsync(order);
    }
    public async Task<Order> RemoveProductFromCart(long itemId, long orderId)
    {
        try{
            var order = await _orderRepository.GetAsync(orderId);
            var item = order.Items.Single(i => i.Id == itemId);
            var product = item.Product;
            order.Total -= product!.Price * item.Quantity;
            order.Items.Remove(item); 
            _ = await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
            return order;
        }catch(Exception e){
            throw new CartServiceException("The Item cannot be deleted from Shopping Cart!", e);
        }
    }

    public async Task<Order> GetCart(long orderId)
    {
        return await _orderRepository.GetAsync(orderId);
    }
}

public class CartServiceException : Exception
{
    public CartServiceException(string message): base(message){}
    public CartServiceException(string message, Exception innerException) : base(message, innerException){}
}