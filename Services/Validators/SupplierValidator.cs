using Contracts;
using FluentValidation;

namespace Services.Validators;

public class SupplierValidator : AbstractValidator<SupplierCreationDTO>
{
    public SupplierValidator()
    {
        RuleFor(supplier => supplier.Name)
            .NotNull()
            .WithMessage("Supplier {ProdpertyName} cannot be null!");
        RuleFor(supplier => supplier.Name)
            .NotEmpty()
            .WithMessage("Supplier {PropertyName} cannot be empty");
        RuleFor(supplier => supplier.Name)
            .Length(5,300)
            .WithMessage("Supplier {PropertyName} must be between 10 and 300 characters!");
        RuleFor(supplier => supplier.BaseUrl)
            .NotNull()
            .WithMessage("Supplier {PropertyName} cannot be null!");
        RuleFor(supplier => supplier.BaseUrl)
            .NotEmpty()
            .WithMessage("Supplier {ProertyName} cannot be empty string!");
        RuleFor(supplier => supplier.BaseUrl)
            .Length(5,200)
            .WithMessage("Supplier {PropertyName} must be between 10 and 200 characters!");
        RuleFor(supplier => supplier.Description)
            .NotNull()
            .WithMessage("Supplier {PropertyName} cannot be null!");
        RuleFor(supplier => supplier.Description)
            .NotEmpty()
            .WithMessage("Supplier {PropertyName} cannot be an empty string!");
        RuleFor(suppler =>  suppler.Description)
            .MaximumLength(200)
            .WithMessage("Supplier {PropertyName} must be less than {MaxLength} length!");         
    }
}