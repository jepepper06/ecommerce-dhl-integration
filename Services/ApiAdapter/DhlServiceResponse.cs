using Newtonsoft.Json;

namespace Services.ApiAdapter;
public class Shipment
{  
    [JsonProperty("id")]
    public string? Id { get; set; }
    [JsonProperty("service")]
    public string? Service { get; set; }
    [JsonProperty("origin.address.addressLocality")]
    public string? OriginAddress { get; set; }
    [JsonProperty("destination.address.addressLocality")]
    public string? DestinationAddress { get; set; }
    [JsonProperty("status.location.address.addressLocality")]
    public string? LastAddress { get; set; }
    [JsonProperty("status.status")]
    public string? Status { get; set; }
    [JsonProperty("status.description")]
    public string? StatusDescription { get; set; }
}

public class DhlServiceResponse
{
    [JsonProperty("shipments")]
    public IEnumerable<Shipment>? Shipments { get; set; }
}
