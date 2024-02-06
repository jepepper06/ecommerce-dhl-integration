namespace Model;

public class Item
{
    public Item(){}
    private Item(
        Product product,
        uint quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    public static Item Create(
        Product product,
        uint quantity
    ){
        return new Item(product,quantity);
    }
    public long Id { get; set;}
    public Product? Product { get; set; }
    public Order? Order { get; set; }
    public uint Quantity { get; set; }
}