using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Services.ApiAdapter;

public class DhlServiceAdapter : IDhlServiceAdapter
{
    
    private HttpClient _httpClient;
    private IDeserializationHelper<DhlServiceResponse> _deserializationHelper;
    private readonly DhlOptions _options;
    public DhlServiceAdapter(IHttpClientFactory httpClientFactory,
        IDeserializationHelper<DhlServiceResponse> deserializationHelper,
        IOptions<DhlOptions> options)
    {
        _httpClient = httpClientFactory.CreateClient("DhlClient");;
        _deserializationHelper = deserializationHelper;
        _options = options.Value;
    }
    public async Task<DhlServiceResponse?> GetDhlServiceResponse(string dhlShipmentId)
    {
        string endpointToBeRequest;

        endpointToBeRequest  = EndpointToBeRequestCreator(dhlShipmentId);
        
        _httpClient = HttpClientConfigureSetting();
        
        var jsonResponse =  await _httpClient.GetAsync(endpointToBeRequest);
        if(jsonResponse.StatusCode == System.Net.HttpStatusCode.OK){
            return _deserializationHelper.GetDeserializedResponse(jsonResponse.ToString()); 
        }else{
            throw new DhlServiceException("An error has ocurred while getting service from DHL!");
        }       
    }
    private string EndpointToBeRequestCreator(string dhlShipmentId)
    {
        return _options.Endpoint 
                + "?" 
                + "trackingNumber" 
                + dhlShipmentId;
    }
    private HttpClient HttpClientConfigureSetting()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("DHL-API-KEY",_options.ApiKey);
        httpClient.DefaultRequestHeaders.Add("Content-Type",DhlOptions.CONTENT_TYPE);
        return httpClient;
    }
}

public interface IDhlServiceAdapter
{
    public Task<DhlServiceResponse?> GetDhlServiceResponse(string dhlShipmentId);
}

// DESERIALIZATION FUNCTIONALITY
public class DhlDeserializationHelper : IDeserializationHelper<DhlServiceResponse>
{
    public  DhlServiceResponse? GetDeserializedResponse(string jsonResponse)
    {
        DhlServiceResponse? deserializedResponse = JsonConvert.DeserializeObject<DhlServiceResponse>(jsonResponse);
        return deserializedResponse;
    }
}
public interface IDeserializationHelper<T>
{
    T? GetDeserializedResponse(string response);
}

public class DhlServiceException : Exception
{
    public DhlServiceException(string message) : base(message){}

    public DhlServiceException(string message, Exception innerException) : base(message, innerException){}
}