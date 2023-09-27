using System.Linq.Expressions;
using AutoMapper;
using Barber.Api.DbContexts;
using Barber.Api.Entities;
using Barber.Api.Models;
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
    public CustomerContext Context;
    public IEnumerable<Customer> GenerateCustomers(int quantidade)
    {
        var nameGender = new Faker().PickRandom<Name.Gender>();
        var gender = new Faker().PickRandom<Gender>();
        var anyValidDate = DateOnly.FromDateTime(new Faker().Date.Past(80, DateTime.Now.AddYears(-18)));

        var customers = new Faker<Customer>("pt_BR")
            .CustomInstantiator(f => new Customer{
                Id = f.IndexFaker+1,
                Name = f.Name.FullName(nameGender),
                Gender = gender,
                BirthdayDate = anyValidDate,
                CPF = f.Person.Cpf()})
            .RuleFor(c => c.Email, (f, c) =>
                f.Internet.Email(c.Name.ToLower()));

        return customers.Generate(quantidade);
    }

    public CustomerRepository GenerateAndSetupCustomerRepository( )
    {
        var mapper = new Mock<IMapper>();
        var dbOptions = new DbContextOptionsBuilder<CustomerContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
        Context = new CustomerContext(dbOptions.Options);
        var repo = new CustomerRepository(Context, mapper.Object);

        return repo;

    }
}