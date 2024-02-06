namespace Model;

public class Product
{
    public Product(){}
    private Product(
        string name,
        double price,
        Supplier supplier,
        string path
    ){
        Name = name;
        Price = price;
        Supplier = supplier;
        Path = path;
    }
    public static Product Create(
        string name,
        double price,
        Supplier supplier,
        string path
    ){
        return new Product( name, price, supplier, path);
    }
    public long Id { get; set; }
    public string Name { get; set;} = string.Empty;
    public double Price { get; set;}
    public Supplier? Supplier { get; set;} = new Supplier();
    public string Path { get; set;} = string.Empty;
}