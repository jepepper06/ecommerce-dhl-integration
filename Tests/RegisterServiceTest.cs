using Model;
using Moq;
using Services.Register;
using Services.Repositories;
using Xunit;

namespace Tests;

public class RegisterServiceTest
{
    private Mock<IRepository<Customer>> _clientRepositoryMock { get; set; }
    private IRegistrationService _registrationService { get; set; }
    public RegisterServiceTest(){
        _clientRepositoryMock = new Mock<IRepository<Customer>>();
        _registrationService = new RegistrationService(_clientRepositoryMock.Object);
        _clientRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Customer>()))
            .ReturnsAsync((Customer c) => c);
        _clientRepositoryMock.Setup(r => r.SaveChangesAsync());
    }
    [Fact]
    private async void RegisterClientTest()
    {
        Customer client = new(){
            Id = 1,
            Name = "ansdkjahnsks",
            Email = "asknlkadns",
            PhoneNumber = 128312908.ToString(),
            Document = "V123123"
        };
        var _client = await _registrationService.RegisterClient(client);
        Assert.Equivalent(client,_client);
    }
}