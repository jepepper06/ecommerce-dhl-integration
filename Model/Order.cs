namespace Model;

public class Order
{
    public Order(){}
    public static Order Create(){
        return new Order();
    }
    public long Id { get; set; }
    public double Total { get; set; }
    public Customer? Customer { get; set; }
    public bool IsPayed { get; set; } = false;
    public string? TrackingNumber { get; set; }
    public ICollection<Item> Items { get; set; } = new List<Item>();
    public void ChangeTotal(double total) 
    {
        Total = total;
    }
    public void ChangePaymentStatus(bool isPayed) 
    {
        IsPayed = isPayed;
    }
}