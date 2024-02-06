namespace Model;

public class Supplier
{
    public Supplier(){}
    private Supplier(
        string name,
        string baseUrl,
        string description){
            Name = name;
            BaseUrl = baseUrl;
            Description = description;
        }
    public static Supplier Create(
        string name,
        string baseUrl,
        string description
    ){
        return new Supplier(name,baseUrl,description);
    }
    public long Id { get; set; }
    public string Name { get; set;} = string.Empty;
    public string BaseUrl { get; set;} = string.Empty;
    public string Description { get; set;} = string.Empty;
}