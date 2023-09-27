using AutoMapper;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using Barber.Api.Repositories;
using FluentAssertions;
using FluentValidation;
using Moq.AutoMock;

namespace Barber.Application.Tests.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandlerTests :IClassFixture<CreateAddressCommandHandlerTestsFixture>
{
    readonly CreateAddressCommandHandlerTestsFixture _fixture;
    private AutoMocker _mocker;
    private CreateAddressCommandHandler _createAddressCommandHandler;

    public CreateAddressCommandHandlerTests(CreateAddressCommandHandlerTestsFixture fixture)
    {
        _fixture = fixture;
        _createAddressCommandHandler = _fixture.GenerateAndSetupCommandHandler();
        _mocker = _fixture.Mocker;
    }
    
    [Fact(DisplayName = "Handle Create Address Successfully")]
    [Trait("Category", "Create Address Command Handler Tests")]
    public async void Handle_WhenCommandIsValidAndCustomerExists_Should_ReturnCreateAddressCommandResponse()
    {
        // Arrange
        var command = _fixture.GenerateValidCommand();
        var repo = _mocker.GetMock<ICustomerRepository>();
        var mapper = _mocker.GetMock<IMapper>();
        var validator = _mocker.GetMock<IValidator<CreateAddressCommand>>();
             
        // Act
        var addressHandled = await _createAddressCommandHandler.Handle(command, CancellationToken.None);
        
             
        // Assert
        addressHandled.Should().BeOfType<CreateAddressCommandResponse>();
        addressHandled.Address.Should().NotBeNull();
        addressHandled.Address.Should().BeOfType<CreateAddressCommandDto>();
        addressHandled.IsSuccessful.Should().BeTrue();
    }
    
}