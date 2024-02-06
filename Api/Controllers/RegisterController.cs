using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Register;
using Contracts;
namespace Api.Controllers;

[ApiController]
[Route("register")]
public class RegistrationController : AppController
{
    private readonly IRegistrationService _registrationService;
    public RegistrationController(
        IRegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    [HttpPost("customer-registry")]
    public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRegistrationDTO customerDto)
    {
        var customer = await _registrationService.RegisterCustomer(customerDto);
        return StandarizedResponse<Customer>(customer,StatusCodes.Status201Created);
    }
}