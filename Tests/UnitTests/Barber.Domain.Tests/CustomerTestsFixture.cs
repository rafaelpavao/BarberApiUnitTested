using Bogus;
using Bogus.DataSets;
using Barber.Api.Entities;
using Bogus.Extensions.Brazil;
using Moq.AutoMock;
namespace Barber.Domain.Tests

{
    [CollectionDefinition(nameof(CustomerCollection))]
    public class CustomerCollection : ICollectionFixture<CustomerTestsFixture>
    {
    }

    public class CustomerTestsFixture : IDisposable
    {
        
        public AutoMocker Mocker;

        public Customer GenerateValidCustomer()
        {
            return GenerateCustomers(1, true).FirstOrDefault();
        }

        public IEnumerable<Customer> GetVariatedCustomers()
        {
            var customers = new List<Customer>();

            customers.AddRange(GenerateCustomers(50, true).ToList());
            customers.AddRange(GenerateCustomers(50, false).ToList());

            return customers;
        }

        public IEnumerable<Customer> GenerateCustomers(int quantidade, bool ativo)
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

        public Customer GenerateInvalidCustomer()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var anyValidDate = DateOnly.FromDateTime(new Faker().Date.Past(1, DateTime.Now.AddYears(1)));

            var customer = new Faker<Customer>("pt_BR")
                .CustomInstantiator(f => new Customer{
                    Id = f.IndexFaker+1,
                    Name = f.Name.FullName(genero),
                    BirthdayDate = anyValidDate,
                    CPF = f.Person.Cpf()})
                .RuleFor(c => c.Email, (f, c) =>
                    f.Internet.Email(c.Name.ToLower()));

            return customer;
        }
        
        public void Dispose()
        {
        }
    }
}


