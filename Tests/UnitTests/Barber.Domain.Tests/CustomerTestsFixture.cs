using Bogus;
using Bogus.DataSets;
using Barber.Api.Entities;
using Bogus.Extensions.Brazil;
using Moq.AutoMock;
namespace Barber.Domain.Tests

{
    

    public class CustomerTestsFixture 
    {
        
        public AutoMocker Mocker;

        public Customer GenerateValidCustomer()
        {
            return GenerateCustomers(1).FirstOrDefault();
        }

        public IEnumerable<Customer> GetVariatedCustomers()
        {
            var customers = new List<Customer>();

            customers.AddRange(GenerateCustomers(50).ToList());
            customers.AddRange(GenerateCustomers(50).ToList());

            return customers;
        }

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
        
    }
}


