using System.Linq.Expressions;
using AutoMapper;
using Barber.Api.DbContexts;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.AutoMock;

namespace Barber.Persistence.Tests.Repositories;

public class CustomerRepositoryTestsFixture
{
    public AutoMocker Mocker;
    
    public IEnumerable<Customer> GenerateCustomers(int quantidade)
    {
        var genero = new Faker().PickRandom<Name.Gender>();
        var anyValidDate = DateOnly.FromDateTime(new Faker().Date.Past(80, DateTime.Now.AddYears(-18)));

        var customers = new Faker<Customer>("pt_BR")
            .CustomInstantiator(f => new Customer{
                Id = f.IndexFaker+1,
                Name = f.Name.FullName(genero),
                BirthdayDate = anyValidDate,
                CPF = f.Person.Cpf()})
            .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Name.ToLower()));

        return customers.Generate(quantidade);
    }

    public CustomerRepository GenerateAndSetupCustomerRepository( Mock<IMapper> mapper , CustomerContext context )
    {

        var repo = new CustomerRepository(context, mapper.Object);

        return repo;

    }
}