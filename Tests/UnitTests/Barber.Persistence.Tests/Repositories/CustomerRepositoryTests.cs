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

    public CustomerRepositoryTests(CustomerRepositoryTestsFixture fixture)
    {
        _fixture = fixture;
        _repo = _fixture.GenerateAndSetupCustomerRepository();
        _context = _fixture.Context;
    }
    
    [Trait("Category", "Customer Repository Tests")]
    [Fact(DisplayName = "Add Customer Test")]
    public async void AddCustomer_WhenCustomerIsValid_Should_CreateCustomerInDbAndReturnVoid()
    {
        // Arrange
        var customer = _fixture.GenerateCustomers(1).FirstOrDefault();
        
        // Act
        _repo.AddCustomer(customer);
        await _repo.SaveChangesAsync();
        var customerFromDb = _context.Customers.FirstOrDefault();

        //Assert
        customerFromDb.Should().NotBeNull();
        customerFromDb.Should().BeOfType<Customer>();
        Assert.Equal(customer, customerFromDb);

    }

    [Trait("Category", "Customer Repository Tests")]
    [Fact(DisplayName = "Get All Customers Test")]
    public async void GetAllCustomer_WhenCustomersExistInDb_Should_ReturnCollectionOfCustomers()
    {
        // Arrange
        var customer = _fixture.GenerateCustomers(11);
        
        // Act
        _context.Customers.AddRange(customer);
        await _repo.SaveChangesAsync();
        var customerFromDb = await _repo.GetAllCustomers();

        //Assert
        customerFromDb.Should().NotBeNull();
        customerFromDb.Should().NotBeEmpty();
        customerFromDb.Count().Should().BeGreaterThan(10);
        customerFromDb.Should().BeOfType<List<Customer>>();
        customerFromDb.Equals(customer);
    }
}