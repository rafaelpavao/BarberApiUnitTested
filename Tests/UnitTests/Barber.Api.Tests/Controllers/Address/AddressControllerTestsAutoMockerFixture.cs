using System;
using Barber.Api.Controllers;
using Moq.AutoMock;
using Moq;
using Bogus;
using Bogus.DataSets;
using Barber.Api.Models;
using FakeItEasy;
using AutoMapper;
using Barber.Api.Features.Addresses.Queries.GetAddressById;
using MediatR;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using FluentAssertions;

namespace Barber.Api.Tests.Controllers.Address
{
    [CollectionDefinition(nameof(AddressControllerAutoMockerCollection))]
    public class AddressControllerAutoMockerCollection : ICollectionFixture<AddressControllerTestsAutoMockerFixture>
    {
    }
    public class AddressControllerTestsAutoMockerFixture : IDisposable
	{
        public AutoMocker Mocker;
        public IMediator Mediator;


        public GetAddressByIdDetailDto GenerateValidQueryResponse()
        {
            return GenerateValidAddresses(1).FirstOrDefault();
        }
        
        public AddressToReturnDto GenerateAddressToReturnDto(){
            return new Faker<AddressToReturnDto>()
                .CustomInstantiator(a => new AddressToReturnDto()
                {
                    Id = a.IndexFaker+1,
                    Street = a.Address.StreetName(),
                    Number = 999,
                    District = a.Address.Direction(),
                    City = a.Address.City(),
                    State = a.Address.State(),
                    CEP = a.Address.ZipCode()
                });
        }

        public GetAddressByIdDetailQuery GenerateValidQuery()
        {
            return new Faker<GetAddressByIdDetailQuery>()
                .CustomInstantiator(a => new GetAddressByIdDetailQuery()
                {
                    CustomerId = a.IndexFaker+1,
                    AddressId = a.IndexFaker+1
                });
        }
        
        public GetAddressByIdDetailQuery GenerateInvalidQuery()
        {
            return new Faker<GetAddressByIdDetailQuery>()
                .CustomInstantiator(a => new GetAddressByIdDetailQuery()
                {
                    CustomerId = a.IndexFaker,
                    AddressId = 0
                });
        }
        public CreateAddressCommand GenerateValidCommand()
        {
            return new Faker<CreateAddressCommand>()
                .CustomInstantiator(a => new CreateAddressCommand()
                {
                    CustomerId = 0,
                    Street = a.Address.StreetName(),
                    Number = 999,
                    District = a.Address.Direction(),
                    City = a.Address.City(),
                    State = a.Address.State(),
                    CEP = a.Address.ZipCode()
                    
                });
        }
        public CreateAddressCommand GenerateInvalidCommand()
        {
            return new Faker<CreateAddressCommand>()
                .CustomInstantiator(a => new CreateAddressCommand()
                {
                    CustomerId = a.IndexFaker+1,
                    Street = a.Address.StreetName(),
                    Number = 999,
                    District = a.Address.Direction(),
                    City = a.Address.City(),
                    State = a.Address.State(),
                    CEP = a.Address.ZipCode()
                });
        }
        
        public CreateAddressCommandResponse GenerateValidCommandResponse()
        {
            return A.Dummy<CreateAddressCommandResponse>();
        }

        public IEnumerable<GetAddressByIdDetailDto> GetVariatedAddresses()
        {
            var addresses = new List<GetAddressByIdDetailDto>();

            addresses.AddRange(GenerateValidAddresses(50).ToList());
            addresses.AddRange(GenerateValidAddresses(50).ToList());

            return addresses;

        }

        public IEnumerable<GetAddressByIdDetailDto> GenerateValidAddresses(int amount)
        {
            var addresses = new Faker<GetAddressByIdDetailDto>("pt_BR")
                .CustomInstantiator(a => new GetAddressByIdDetailDto(){
                    Id = a.IndexFaker+1,
                    Street = a.Address.StreetName(),
                    Number = 999,
                    District = a.Address.Direction(),
                    City = a.Address.City(),
                    State = a.Address.State(),
                    CEP = a.Address.ZipCode()
            });
            return addresses.Generate(amount);
        }

        public IMediator GenerateMediatorMock()
        {
            Mediator = A.Fake<IMediator>();
            A.CallTo(() => Mediator.Send(It.IsAny<GetAddressByIdDetailQuery>(), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(A.Dummy<GetAddressByIdDetailDto>()));
            A.CallTo(() => Mediator.Send(It.IsAny<IRequest>(),A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(GenerateValidCommandResponse()));
            
           
            return Mediator;
            // Mocker = new AutoMocker();
            // Mediator = Mocker.CreateInstance<IMediator>();
            // Mocker.GetMock<IMediator>()
            //     .Setup(m => m.Send(It.IsAny<GetAddressByIdDetailQuery>, CancellationToken.None))
            //     .Callback<IRequest<GetAddressByIdDetailQuery>, CancellationToken>((query, ct) => query.Should().BeEquivalentTo(GenerateValidQuery()))
            //     .ReturnsAsync(GenerateValidQueryResponse());
            // Mocker.GetMock<IMediator>()
            //     .Setup(m => m.Send(It.IsAny<CreateAddressCommand>, CancellationToken.None))
            //     .Callback<IRequest<CreateAddressCommand>, CancellationToken>((command, ct) => command.Should().BeEquivalentTo(GenerateValidCommand()))
            //     .ReturnsAsync(GenerateValidCommandResponse());

            return Mediator;
            // .Setup(m => m.Send<GetAddressByIdDetailQuery>(It.IsAny<IRequest>, CancellationToken.None)).ReturnsAsync(GenerateValidQueryResponse());
        }

        public void Dispose(){}
    }
}

