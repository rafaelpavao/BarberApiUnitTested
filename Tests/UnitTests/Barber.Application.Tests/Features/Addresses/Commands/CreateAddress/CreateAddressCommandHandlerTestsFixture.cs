using AutoMapper;
using Barber.Api.Entities;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using Barber.Api.Repositories;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using FluentValidation;
using Moq;
using Moq.AutoMock;
using Address = Barber.Api.Entities.Address;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Barber.Application.Tests.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandlerTestsFixture
{
    public AutoMocker Mocker;
    
    //vem createaddresscommand, ct
    //sai createaddresscommandresponse
    
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

    public Customer GenerateValidCustomerEntity()
    {
        var genero = new Faker().PickRandom<Name.Gender>();
        var anyValidDate = DateOnly.FromDateTime(new Faker().Date.Past(80, DateTime.Now.AddYears(-18)));

        var customer = new Faker<Customer>("pt_BR")
            .CustomInstantiator(f => new Customer
            {
                Id = f.UniqueIndex+1,
                Name = f.Name.FullName(genero),
                BirthdayDate = anyValidDate,
                CPF = f.Person.Cpf(),
            })
            .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Name.ToLower()));
        return customer;

    }

    public Address GenerateValidAddressEntity()
    {
        var address = new Faker<Address>("pt_BR").CustomInstantiator(a => new Address()
        {
            CEP = a.Address.ZipCode(),
            State = a.Address.State(),
            Street = a.Address.StreetName(),
            District = a.Address.Direction(),
            CustomerId = 1,
            City = a.Address.City(),
            Number = a.IndexFaker + 1
        });
        return address;
    }
    
    public CreateAddressCommandDto GenerateValidCommandDto()
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

    public ValidationResult GenerateValidValidationResult()
    {
        return new Faker<ValidationResult>().Generate();
    }

    public CreateAddressCommandHandler GenerateAndSetupCommandHandler()
    {
        Mocker = new AutoMocker();

        var createAddressCommandHandler = Mocker.CreateInstance<CreateAddressCommandHandler>();

        Mocker.GetMock<IValidator<CreateAddressCommand>>()
            .Setup(v => v.Validate(It.IsAny<CreateAddressCommand>()))
            .Returns(GenerateValidValidationResult);

        Mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.AddAddress(It.IsAny<Customer>(), It.IsAny<Address>())).Verifiable();
        
        Mocker.GetMock<ICustomerRepository>()
            .Setup(r => r.GetCustomerWithAddressesById(It.IsAny<int>()))
            .ReturnsAsync(GenerateValidCustomerEntity);

        Mocker.GetMock<IMapper>()
            .Setup(m => m.Map<CreateAddressCommandDto>(It.IsAny<Address>()))
            .Returns(GenerateValidCommandDto);

        Mocker.GetMock<IMapper>()
            .Setup(m => m.Map<Address>(It.IsAny<CreateAddressCommand>()))
            .Returns(GenerateValidAddressEntity);

        return createAddressCommandHandler;
    }
}