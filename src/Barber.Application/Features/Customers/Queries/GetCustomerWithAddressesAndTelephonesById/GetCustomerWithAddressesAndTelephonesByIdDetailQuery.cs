using MediatR;

namespace Barber.Api.Features.Customers.Queries.GetCustomerWithAddressesAndTelephonesById;

public class GetCustomerWithAddressesAndTelephonesByIdDetailQuery : IRequest<GetCustomerWithAddressesAndTelephonesByIdDetailDto>{
  public int CustomerId { get; set; }
}