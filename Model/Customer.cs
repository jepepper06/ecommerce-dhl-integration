using System.Text.Json.Serialization;

namespace Model;

public class Customer 
{
    private Customer(
        string email,
        string name,
        string document,
        string phoneNumber)
    {   
        Email = email;
        Name = name;
        Document = document;
        PhoneNumber = phoneNumber;
    }
    public Customer()
    {
    }
    public static Customer Create(
        string email,
        string name,
        string document,
        string phoneNumber
    ){
        return new Customer( email, name, document, phoneNumber);
    }
    public long Id{ get; set; }
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
	[JsonIgnore]
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public void AddToOrders(Order order){
        Orders.Add(order);
    }
    public void ChangePersonalData(string email, string phoneNumber) {
        Email = email;
        PhoneNumber = phoneNumber;
    }
}