using Contracts;
using FluentValidation;
using FluentValidation.Internal;
using Model;

namespace Services.Validators;

public class ProductValidator : AbstractValidator<ProductCreationDTO>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product {PropertyName} cannot be an empty string!");
        RuleFor(product => product.Name)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
        RuleFor(product => product.Name)
            .Length(2,255)
            .WithMessage("Product {PropertyName} could be between 10 and 255 characters long!");
        RuleFor(product => product.Price)
            .GreaterThan(0.0)
            .WithMessage("Product {PropertyName} is less than {ComparisonValue}!");
        RuleFor(product => product.Price)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
        RuleFor(product => product.Path)
            .NotEmpty()
            .WithMessage("Product {Path} cannot be empty!");
        RuleFor(product => product.Path)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
        RuleFor(product => product.Path)
            .MinimumLength(5)
            .WithMessage("Product {PropertyName} cannot be less than {MinLength} characters long!");
        RuleFor(product => product.SupplierId)
            .NotNull()
            .WithName("Product {PropertyName} cannot be null!");
        RuleFor(product => product.SupplierId)
            .GreaterThan(0)
            .WithMessage("Product {PropertyName} cannot be less than 1");
    }
}

public class ProductUpdateValidator : AbstractValidator<ProductUpdateDTO>
{
    public ProductUpdateValidator(){
        RuleFor(product => product.Id)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
        RuleFor(product => product.Id)
            .GreaterThan(0)
            .WithMessage("Product {PropertyName} cannot be less than 0!");
        RuleFor(product => product.Name)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product {PropertyName} cannot be an empty string!");
        RuleFor(product => product.Name)
            .Length(10,400)
            .WithMessage("Product {PropertyName} should be between {MinLength} and {MaxLength} long!");
        RuleFor(product => product.Price)
            .GreaterThanOrEqualTo(0.1)
            .WithMessage("Product {PropertyName} should be greater than 0.1");
        RuleFor(product => product.Price)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
        RuleFor(product => product.SupplierId)
            .GreaterThan(0)
            .WithMessage("Product {PropertyName} cannot be less than {PropertyValue}!");
        RuleFor(product => product.SupplierId)
            .NotNull()
            .WithMessage("Product {PropertyName} cannot be null!");
    }
}
