namespace Contracts;

public class CustomerRegistrationDTO
{
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Document { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}