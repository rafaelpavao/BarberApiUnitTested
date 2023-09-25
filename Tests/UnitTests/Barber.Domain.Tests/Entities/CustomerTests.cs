using Barber.Domain.Tests;
using FluentAssertions;
using Xunit.Abstractions;
namespace Barber.Domain.Tests.Entities.Entities
{
    [Collection(nameof(CustomerCollection))]
    public class CustomerFluentAssertionsTests
    {
        private readonly CustomerTestsFixture _customerTestsFixture;
        readonly ITestOutputHelper _outputHelper;

        public CustomerFluentAssertionsTests(CustomerTestsFixture customerTestsFixture, 
                                            ITestOutputHelper outputHelper)
        {
            _customerTestsFixture = customerTestsFixture;
            _outputHelper = outputHelper;
        }
        

        [Fact(DisplayName = "Novo Customer Válido")]
        [Trait("Categoria", "Customer Fluent Assertion Testes")]
        public void Customer_NovoCustomer_DeveEstarValido()
        {
            // Arrange
            var customer = _customerTestsFixture.GenerateValidCustomer();

            // Act
            var result = customer.IsValid();

            // Assert 
            result.Should().BeTrue();
            customer.ValidationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = "Novo Customer Inválido")]
        [Trait("Categoria", "Customer Fluent Assertion Testes")]
        public void Customer_NovoCustomer_DeveEstarInvalido()
        {
            // Arrange
            var customer = _customerTestsFixture.GenerateInvalidCustomer();

            // Act
            var result = customer.IsValid();

            // Assert 
            result.Should().BeFalse();
            customer.ValidationResult.Errors.Should().HaveCountGreaterOrEqualTo(1, "deve possuir erros de validação");

            _outputHelper.WriteLine($"Error found: {customer.ValidationResult.Errors.FirstOrDefault().ErrorMessage}");
        }
    }
}