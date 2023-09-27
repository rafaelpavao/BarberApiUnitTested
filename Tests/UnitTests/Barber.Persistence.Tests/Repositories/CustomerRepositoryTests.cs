using AutoMapper;
using Barber.Api.DbContexts;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.AutoMock;

namespace Barber.Persistence.Tests.Repositories;

public class CustomerRepositoryTests : IClassFixture<CustomerRepositoryTestsFixture>
{
    private readonly CustomerRepositoryTestsFixture _fixture;
    private CustomerRepository _repo;
    private CustomerContext _context;
    private Mock<IMapper> _mapper;

    public CustomerRepositoryTests(CustomerRepositoryTestsFixture fixture)
    {
        _fixture = fixture;
        _mapper = new Mock<IMapper>();
        var dbOptions = new DbContextOptionsBuilder<CustomerContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
        _context = new CustomerContext(dbOptions.Options);
        _repo = new CustomerRepository(_context, _mapper.Object);
    }

    [Trait("Category", "Customer Repository Tests")]
    [Fact(DisplayName = "Get All Customers Test")]
    public async void GetAllCustomer_WhenCustomersExistInDb_Should_ReturnCollectionOfCustomers()
    {
        // Arrange
        var customer = _fixture.GenerateCustomers(1).FirstOrDefault();
        // await _context.Customers.AddRangeAsync(customers);
        
        // Act
        _repo.AddCustomer(customer);
        var customerFromDb = _context.Customers.ToList();

        //Assert
        customerFromDb.Should().NotBeNull();
        customerFromDb.Should().NotBeEmpty();
        customerFromDb.Count.Should().BeGreaterThan(0);
        customerFromDb.Should().BeOfType<IEnumerable<Customer>>();
        customerFromDb.FirstOrDefault().Equals(customer);

    }
}