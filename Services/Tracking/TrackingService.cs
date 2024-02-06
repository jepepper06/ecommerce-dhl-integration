using Model;
using Services.ApiAdapter;
using Services.Repositories;

namespace Services;

public interface ITrackingService
{
    Task<DhlServiceResponse> GetTrackingInformation(string trackingId);
}
// LACKS OF TESTING

public class TrackingService : ITrackingService
{
    private readonly IDhlServiceAdapter _dhlServiceAdapter;
    private readonly IRepository<Order> _orderRepository;
    public TrackingService(
        IDhlServiceAdapter dhlServiceAdapter,
        IRepository<Order> orderRepository){
        _dhlServiceAdapter = dhlServiceAdapter;
        _orderRepository = orderRepository;
    }

    public async Task<DhlServiceResponse> GetTrackingInformation(string trackingId)
    {
        var dhlResponse = await _dhlServiceAdapter.GetDhlServiceResponse(trackingId);
        return  dhlResponse!;
    }
}