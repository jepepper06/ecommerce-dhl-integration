using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Contracts;
using FluentValidation;
using Model;
using Services.Repositories;

namespace Services.Admin;

public interface IAdminService
{
    Task<IEnumerable<Customer>> GetCustomers(int pageNumber);
    Task<Product> UpdateProduct(ProductUpdateDTO productDto);
    Task<Product> CreateProduct(ProductCreationDTO productDto);
    Task<Order> UpdateOrder(OrderUpdateDTO orderDto);
    Task<Customer> UpdateCustomer(CustomerUpdateDTO customerDto);
    Task<Supplier> CreateSupplier(SupplierCreationDTO supplierDto);
}
public class AdminService : IAdminService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Customer> _customerRepository;
    private readonly IRepository<Supplier> _supplierRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<ProductCreationDTO> _productCreationValidator;
    private readonly IValidator<SupplierCreationDTO> _supplierValidator;
    private readonly IValidator<ProductUpdateDTO> _productUpdateValidator;
    public AdminService(
        IRepository<Product> productRepository,
        IRepository<Order> orderRepository,
        IRepository<Customer> customerRepository,
        IRepository<Supplier> supplierRepository,
        IMapper mapper,
        IValidator<ProductCreationDTO> productCreationValidator,
        IValidator<SupplierCreationDTO> supplierValidator,
        IValidator<ProductUpdateDTO> productUpdateValidator
    ){
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _supplierRepository = supplierRepository;
        _mapper = mapper;
        _supplierValidator = supplierValidator;
        _productCreationValidator = productCreationValidator;
        _productUpdateValidator = productUpdateValidator;
    }

    public async Task<Product> CreateProduct(ProductCreationDTO productDto)
    {
        var validationTask = _productCreationValidator.ValidateAndThrowAsync(productDto);
        var product = _mapper.Map<ProductCreationDTO,Product>(productDto);
        product.Supplier = await _supplierRepository.GetAsync(productDto.SupplierId);
        await _productRepository.AddAsync(product);
        await _productRepository.SaveChangesAsync();
        await validationTask;
        return product; 
    }

    public async Task<IEnumerable<Customer>> GetCustomers(int pageNumber)
    {
        return await _customerRepository.GetAllAsync(pageNumber);
    }

    public async Task<Customer> UpdateCustomer(CustomerUpdateDTO clientDto)
    {
        var client = await _customerRepository.GetAsync(clientDto.Id);
        client.ChangePersonalData(clientDto.Email!, clientDto.PhoneNumber!);
        await _customerRepository.UpdateAsync(client);
        await _customerRepository.SaveChangesAsync();
        return client;
    }

    public async Task<Order> UpdateOrder(OrderUpdateDTO orderDto)
    {
        var order = await _orderRepository.GetAsync(orderDto.Id);
        order.ChangeTotal(orderDto.Total);
        order.ChangePaymentStatus(orderDto.IsPayed);
        await _orderRepository.UpdateAsync(order);
        await _orderRepository.SaveChangesAsync();
        return order;
    }

    public async Task<Product> UpdateProduct(ProductUpdateDTO productDto)
    {
        var productUpdateValidationTask = _productUpdateValidator.ValidateAndThrowAsync(productDto);
        var product = _mapper.Map<ProductUpdateDTO,Product>(productDto);
        product.Supplier = await _supplierRepository.GetAsync(productDto.Id);
        await _productRepository.UpdateAsync(product);
        await productUpdateValidationTask;
        await _productRepository.SaveChangesAsync();
        return product;
    }
    public async Task<Supplier> CreateSupplier(SupplierCreationDTO supplierDto)
    {
        var validatorTask = _supplierValidator.ValidateAndThrowAsync(supplierDto);
        var supplier = _mapper.Map<SupplierCreationDTO,Supplier>(supplierDto);
        await _supplierRepository.AddAsync(supplier);
        await validatorTask;
        await _supplierRepository.SaveChangesAsync();
        return supplier;
    }
}