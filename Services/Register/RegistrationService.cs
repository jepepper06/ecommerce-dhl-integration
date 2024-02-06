using AutoMapper;
using Contracts;
using FluentValidation;
using Model;
using Services.Repositories;
namespace Services.Register;

public interface IRegistrationService
{
    Task<Customer> RegisterCustomer(CustomerRegistrationDTO clientDto);    
}

public class RegistrationService : IRegistrationService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IValidator<CustomerRegistrationDTO>  _validator;
    private readonly IMapper _mapper;
    public RegistrationService(
        IRepository<Customer> customerRepository,
        IMapper mapper,
        IValidator<CustomerRegistrationDTO> validator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }
    public async Task<Customer> RegisterCustomer(CustomerRegistrationDTO customerDto)
    {
        var valitadorTask = _validator.ValidateAndThrowAsync(customerDto);
        var customer = _mapper.Map<CustomerRegistrationDTO,Customer>(customerDto);
        await _customerRepository.AddAsync(customer);
        await valitadorTask;
        await _customerRepository.SaveChangesAsync();
        return customer!;
    }
}