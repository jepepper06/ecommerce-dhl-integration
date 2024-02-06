namespace Services.ApiAdapter;

public class DhlOptions
{
    public const string DHL = "DhlOptions";
    public const string CONTENT_TYPE = "application/json";
    public string? Endpoint { get; set; }
    public string? ApiKey { get; set; }
}