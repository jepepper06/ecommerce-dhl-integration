using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Shopping;
using Contracts;

namespace Api.Controllers;
[ApiController]
[Route("shopping")]
public class ShoppingController : AppController
{
    private readonly ShoppingService _shoppingService;
    public ShoppingController(ShoppingService shoppingService )
    {
        _shoppingService = shoppingService;
    }
    [HttpGet("product")]
    public async Task<IActionResult> GetProductByName([FromQuery] string name)
    {
        var product = await _shoppingService.GetProductByName(name);
        return StandarizedResponse<ProductPublicDTO>(product,StatusCodes.Status200OK);
    }
    [HttpGet("products")]
    public async Task<IActionResult> GetProductsPaginated(uint pageNumber)
    {
        var products = await _shoppingService.GetProductsPaginated((int) pageNumber);
        return StandarizedResponse<ProductPublicDTO>(products,StatusCodes.Status200OK);
    }
}