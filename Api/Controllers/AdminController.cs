using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Admin;
using Services.Validators;
using Contracts;

namespace Api.Controllers;

[ApiController]
[Route("admin")]
public class AdminController : AppController
{
    private readonly IAdminService _adminService;

    public AdminController(
        IAdminService adminService)
    {
        _adminService = adminService;
    }
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers(int pageNumber)
    {
        var customers =  await _adminService.GetCustomers(pageNumber);
        return StandarizedResponse<Customer>(customers,StatusCodes.Status200OK);
    }
    [HttpPut("product")]
    public async Task<IActionResult> UpdateProduct(ProductUpdateDTO productDto)
    {
        var product =  await _adminService.UpdateProduct(productDto);
        return StandarizedResponse<Product>(product,StatusCodes.Status202Accepted);
    }
    [HttpPost("product")]
    public async Task<IActionResult> CreateProduct(ProductCreationDTO productDto)
    {
        var product =  await _adminService.CreateProduct(productDto);
        return StandarizedResponse<Product>(product,StatusCodes.Status201Created);
    }
    [HttpPut("client")]
    public async Task<IActionResult> UpdateClient(CustomerUpdateDTO customerDto)
    {
        var customer =  await _adminService.UpdateCustomer(customerDto);
        return StandarizedResponse<Customer>(customer,StatusCodes.Status202Accepted);
    }
    [HttpPut("order")]
    public async Task<IActionResult> UpdateOrder(OrderUpdateDTO orderDto)
    {
        var order = await _adminService.UpdateOrder(orderDto);
        return StandarizedResponse<Order>(order,StatusCodes.Status202Accepted);
    }
	[HttpPost("supplier")]
    public async Task<IActionResult> CreateSupplier(SupplierCreationDTO supplierDto) 
    {
        var supplier = await _adminService.CreateSupplier(supplierDto);
        return StandarizedResponse<Supplier>(supplier,StatusCodes.Status201Created);
    }
}
