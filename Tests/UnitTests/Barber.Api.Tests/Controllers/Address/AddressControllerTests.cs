using Barber.Api.Controllers;
using Barber.Api.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using Barber.Api.Features.Addresses.Queries.GetAddressById;
using FakeItEasy;
using Times = Moq.Times;

namespace Barber.Api.Tests.Controllers.Address

{
    [Collection(nameof(AddressControllerAutoMockerCollection))]
        public class AddressControllerAutoMockerFixtureTests
        {

            readonly AddressControllerTestsAutoMockerFixture _addressControllerTestsAutoMockerFixture;
            private AutoMocker _mocker;
            private AddressController _addressController;


            public AddressControllerAutoMockerFixtureTests(AddressControllerTestsAutoMockerFixture addressControllerTestsAutoMockerFixture)
            {
                _addressControllerTestsAutoMockerFixture =  addressControllerTestsAutoMockerFixture;
                _addressController = _addressControllerTestsAutoMockerFixture.GenerateAndSetupAddressController();
                _mocker = _addressControllerTestsAutoMockerFixture.Mocker;
            }


         [Fact(DisplayName = "Get Address Successfully")]
         [Trait("Categoria", "Address Controller AutoMockFixture Tests")]
         public async void AddressController_GetAddressById_Should_RunSuccessfully()
         {
             // Arrange
             var query = _addressControllerTestsAutoMockerFixture.GenerateValidQuery();
             
             // Act
             var addressReturned = await _addressController.GetAddressById(query.CustomerId, query.AddressId);
             var addressResult = addressReturned.Result;
             var addressValue = (GetAddressByIdDetailDto)((OkObjectResult)addressResult).Value;
             
             // Assert
             addressReturned.Result.Should().BeOfType<OkObjectResult>();
             addressValue.Should().BeAssignableTo<GetAddressByIdDetailDto>();
             addressReturned.Result.Should().NotBeNull();
             Assert.IsType<ActionResult<GetAddressByIdDetailDto>>(addressReturned);
             _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<GetAddressByIdDetailQuery>(), CancellationToken.None),
                 Times.Once);
         }

        [Fact(DisplayName = "Create Address Successfully")]
            [Trait("Categoria", "Address Controller AutoMockFixture Tests")]
            public async void AddressController_CreateAddress_Should_CreateAddressSuccessfully()
            {
                // Arrange
                var command = _addressControllerTestsAutoMockerFixture.GenerateValidCommand();
                
                // Act
                var addressCreatedResponse = await  _addressController.CreateAddress(command.CustomerId, command);
                var addressCreatedResult = addressCreatedResponse.Result;
                var addressValue = (CreateAddressCommandDto)((CreatedAtRouteResult)addressCreatedResult).Value;
                
                // Assert
                addressCreatedResult.Should().BeOfType<CreatedAtRouteResult>();
                addressValue.Should().BeAssignableTo<CreateAddressCommandDto>();
                addressValue.Should().NotBeNull();
                _mocker.GetMock<IMediator>().Verify(m => m.Send(It.IsAny<CreateAddressCommand>(), CancellationToken.None),
                    Times.Once);
            }
           
    }
    }

