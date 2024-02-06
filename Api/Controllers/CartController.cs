using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Cart;

namespace Api.Controllers;
[ApiController]
[Route("cart")]
public class CartController : AppController
{
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPut("{productId:long}/{quantity:uint}/{clientId:long}")]
    public async Task<IActionResult> AddItemToCart(long productId, uint quantity, long clientId)
    {
        var order = await _cartService.AddProductToCart(productId,quantity,clientId);
        return StandarizedResponse<Order>(order,StatusCodes.Status202Accepted);
    }
    [HttpPut("{itemId:long}/{orderId:long}")]
    public async Task<IActionResult> RemoveItemFromCart(long itemId, long orderId)
    {
        var order = await _cartService.RemoveProductFromCart(itemId,orderId);
        return StandarizedResponse<Order>(order,StatusCodes.Status202Accepted);
    }
    [HttpGet("{orderId:long}")]
    public async Task<IActionResult> GetCart(long orderId)
    {
        return StandarizedResponse<Order>(await _cartService.GetCart(orderId),StatusCodes.Status200OK);
    }
}