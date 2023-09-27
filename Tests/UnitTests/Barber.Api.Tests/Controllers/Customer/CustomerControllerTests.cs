using Barber.Api.Controllers;
using Barber.Api.Features.Customers.Queries.GetAllCustomers;
using Barber.Api.Tests.Controllers.Address;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq.AutoMock;

namespace Barber.Api.Tests.Controllers.Customer;

public class CustomerControllerTests : IClassFixture<CustomerControllerTestsFixture>
{

    readonly CustomerControllerTestsFixture _fixture;
    private AutoMocker _mocker;
    private CustomerController _customerController;

    public CustomerControllerTests(CustomerControllerTestsFixture fixture)
    {
        _fixture = fixture;
        _customerController = _fixture.GenerateAndSetupCustomerController();
        _mocker = _fixture.Mocker;
    }


    [Fact(DisplayName = "Get all Customers Successfully" )]
    [Trait("Categoria", "Customer Controller Tests")]
    public async void GetAllCustomers_WhenCustomersAreNotNull_ShouldReturnOkCustomers()
    {

        // Arrange
        
        // Act
        var customerResponse = await _customerController.GetAllCustomers();
        var customerResult = customerResponse.Result;
        var customerValue = (IEnumerable<GetAllCustomersDetailDto>)
            ((OkObjectResult)customerResult).Value;
        
        // Assert
        customerResult.Should().BeOfType<OkObjectResult>();
        customerValue.Count().Should().BeGreaterThan(0);
        customerValue.Should().BeAssignableTo<IEnumerable<GetAllCustomersDetailDto>>();
    }


}