namespace Contracts;

public class ProductCreationDTO
{
    public ProductCreationDTO(){}
    public string? Name{ get; set; }
    public double Price{ get; set; }
    public long SupplierId { get; set; }
    public string? Path { get; set; }
}
