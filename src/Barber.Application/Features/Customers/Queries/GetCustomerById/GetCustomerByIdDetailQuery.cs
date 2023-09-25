using MediatR;

namespace Barber.Api.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdDetailQuery : IRequest<GetCustomerByIdDetailDto>{
  public int CustomerId { get; set; }
}