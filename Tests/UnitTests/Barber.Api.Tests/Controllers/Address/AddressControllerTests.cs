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
            private readonly IMediator _mediator;
            private AddressController _addressController;


            public AddressControllerAutoMockerFixtureTests(AddressControllerTestsAutoMockerFixture addressControllerTestsAutoMockerFixture)
            {
                _addressControllerTestsAutoMockerFixture = addressControllerTestsAutoMockerFixture;
                _mediator = _addressControllerTestsAutoMockerFixture.GenerateMediatorMock();
                _addressController = new AddressController(_mediator);

            }


         [Fact(DisplayName = "Get Address Successfully")]
         [Trait("Categoria", "Address Contyroller AutoMockFixture Tests")]
         public async void AddressController_GetAddressById_Should_RunSuccessfully()
         {
             // Arrange
             var query = _addressControllerTestsAutoMockerFixture.GenerateValidQuery();
             var address = _addressControllerTestsAutoMockerFixture.GenerateValidQueryResponse();
             
             
             // Act
             var addressReturned = _addressController.GetAddressById(query.CustomerId, query.AddressId);
             var mediatorQueryResult = await _mediator.Send(query);

             // Assert
             //addressReturned.Value.Should().NotBeNull();
             address.Should().BeOfType<GetAddressByIdDetailDto>();
             // mediatorQueryResult.Should().BeEquivalentTo(address);
             Assert.IsType<Task<ActionResult<AddressToReturnDto>>>(addressReturned);
             //A.CallTo(() => _mediator.Send(It.IsAny<GetAddressByIdDetailQuery>(), A<CancellationToken>.Ignored))
                 //.MustHaveHappenedTwiceExactly();
             //_addressController.Verify(c => c.GetAddressById(It.IsAny<int>, It.IsAny<int>), Times.Once);
         }

        [Fact(DisplayName = "Create Address Successfully")]
            [Trait("Categoria", "Address Contyroller AutoMockFixture Tests")]
            public void AddressController_CreateAddress_Should_CreateAddressSuccessfully()
            {
                // Arrange
                
                var address = _addressControllerTestsAutoMockerFixture.GenerateValidCommandResponse();
                var command = _addressControllerTestsAutoMockerFixture.GenerateValidCommand();
                
                // Act
                var mediatorResult = _mediator.Send(command);
                var addressCreatedReturn = _addressController.CreateAddress(command.CustomerId, command);
                
                // Assert

                addressCreatedReturn.Should().BeOfType<Task<ActionResult<CreateAddressCommandDto>>>();
                //addressCreatedReturn.Value.Should().NotBeNull();
                mediatorResult.Result.Errors.Count.Should().Be(0);
                Assert.True(mediatorResult.Result.IsSuccessful);
                A.CallTo(() => _mediator.Send(command,A<CancellationToken>.Ignored)).MustHaveHappenedTwiceExactly();
            }
            //
        //[Fact(DisplayName = "Obter Clientes Ativos")]
        //[Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        //public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        //{
        //    // Arrange
        //    _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos())
        //        .Returns(_clienteTestsAutoMockerFixture.ObterClientesVariados());
        //
        //    // Act
        //    var clientes = _clienteService.ObterTodosAtivos();
        //
        //    // Assert 
        //    _clienteTestsAutoMockerFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
        //    Assert.True(clientes.Any());
        //    Assert.False(clientes.Count(c => !c.Ativo) > 0);
        //}
    }
    }

