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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace Barber.Api.Tests.Controllers.Address
{
    [CollectionDefinition(nameof(AddressControllerAutoMockerCollection))]
    public class AddressControllerAutoMockerCollection : ICollectionFixture<AddressControllerTestsAutoMockerFixture>
    {
    }
    public class AddressControllerTestsAutoMockerFixture : IDisposable
	{
        public AutoMocker Mocker;
        // public Mediator Mediator;
        public AddressController AddressController;


        public GetAddressByIdDetailDto GenerateValidQueryResponse()
        {
            return GenerateValidAddresses(1).FirstOrDefault();
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
        
        public CreateAddressCommand GenerateValidCommand()
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
        public CreateAddressCommandDto GenerateInvalidCommand()
        {
            return new Faker<CreateAddressCommandDto>()
                .CustomInstantiator(a => new CreateAddressCommandDto()
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
        
        public CreateAddressCommandResponse GenerateValidCommandResponse()
        {
            return new Faker<CreateAddressCommandResponse>().CustomInstantiator(a => new CreateAddressCommandResponse()
            {
                Errors = new Faker<Dictionary<string, string[]>>().Generate(),
                IsSuccessful = true,
                Address = GenerateInvalidCommand()
            });
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

        public AddressController GenerateAndSetupAddressController()
        {
            Mocker = new AutoMocker();
            AddressController = Mocker.CreateInstance<AddressController>();
            
            Mocker.GetMock<IMediator>()
                .Setup(m => m.Send(It.IsAny<GetAddressByIdDetailQuery>(), CancellationToken.None))
                .Returns(Task.FromResult(GenerateValidQueryResponse()));
            Mocker.GetMock<IMediator>()
                .Setup(m => m.Send(It.IsAny<CreateAddressCommand>(), CancellationToken.None))
                .ReturnsAsync(GenerateValidCommandResponse());

            return AddressController;
        }

        public void Dispose(){}
    }
}

