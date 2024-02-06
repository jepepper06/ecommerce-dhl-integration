namespace Contracts;

public class ProductUpdateDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public long SupplierId { get; set; }
}