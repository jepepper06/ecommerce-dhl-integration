using Contracts;
using FluentValidation;
using Model;

namespace Services.Validators;

public class CustomerValidator : AbstractValidator<CustomerRegistrationDTO>
{
    public CustomerValidator()
    {

        RuleFor(client => client.Document)
            .NotEmpty()
            .WithMessage("Please your {PropertyName} cannot be Empty!");
        RuleFor(client => client.Document)
            .MinimumLength(3)
            .WithMessage("Please your Document should have more than {MinLength}!");
        RuleFor(client => client.Email)
            .EmailAddress()
            .WithMessage("{PropertyName} is not a valid Email!");
        RuleFor(client => client.Name)
            .NotEmpty()
            .WithMessage("Client {PropertyName} cannot be empty!");
        RuleFor(client => client.Name)
            .MinimumLength(10)
            .WithMessage("Client {PropertyName} cannot be less than {MinLength} characters long!");
        RuleFor(client => client.PhoneNumber)
            .MinimumLength(8)
            .WithMessage("Client {PropertyName} cannot be less than {MinLength} characters long!");
    }
}
public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateDTO> 
{
    public CustomerUpdateValidator() 
    {
        RuleFor(client => client.Id)
            .NotNull()
            .WithMessage("Client {PropertyName} cannot be null!");
        RuleFor(client => client.Id)
            .GreaterThan(0)
            .WithMessage("Client {PropertyName} should be greater than 0");
        // work to be done here
    }
}