using Barber.Api.Controllers;
using Bogus.DataSets;
using Bogus;
using Moq.AutoMock;
using Barber.Api.Features.Customers.Queries.GetAllCustomers;
using Barber.Api.Models;
using Bogus.Extensions.Brazil;
using MediatR;
using Moq;

namespace Barber.Api.Tests.Controllers.Customer;

public class CustomerControllerTestsFixture 
{
    public AutoMocker Mocker;

    public GetAllCustomersDetailDto GenerateValidCustomer()
    {
        return GenerateCustomers(1).FirstOrDefault();
    }


    public List<GetAllCustomersDetailDto> GenerateCustomers(int quantidade)
    {
        var gender = new Faker().PickRandom<Name.Gender>();
        var genderDto = new Faker().PickRandom<GenderDto>();
        var anyValidDate = DateOnly.FromDateTime(new Faker().Date.Past(80, DateTime.Now.AddYears(-18)));

        var customers = new Faker<GetAllCustomersDetailDto>("pt_BR")
            .CustomInstantiator(f => new GetAllCustomersDetailDto
            {
                Id = f.IndexFaker + 1,
                Name = f.Name.FullName(gender),
                BirthdayDate = anyValidDate,
                Gender = genderDto,
                CPF = f.Person.Cpf()
            })
            .RuleFor(c => c.Email, (f, c) =>
                  f.Internet.Email(c.Name.ToLower()));

        return customers.Generate(quantidade);
    }

    public GetAllCustomersDetailDto GenerateInvalidCustomer()
    {
        var gender = new Faker().PickRandom<Name.Gender>();
        var anyInvalidDate = DateOnly.FromDateTime(new Faker().Date.Past(1, DateTime.Now.AddYears(1)));

        var customer = new Faker<GetAllCustomersDetailDto>("pt_BR")
            .CustomInstantiator(f => new GetAllCustomersDetailDto
            {
                Id = f.IndexFaker + 1,
                Name = f.Name.FullName(gender),
                BirthdayDate = anyInvalidDate,
                CPF = "11111"
            })
            .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Name.ToLower()));

        return customer;
    }

    public CustomerController GenerateAndSetupCustomerController()
    {
        Mocker = new AutoMocker();
        var customerController = Mocker.CreateInstance<CustomerController>();

        Mocker.GetMock<IMediator>().Setup(m => m.Send(It.IsAny<GetAllCustomersDetailQuery>(), CancellationToken.None))
            .ReturnsAsync(GenerateCustomers(50));

        return customerController;
    }
}