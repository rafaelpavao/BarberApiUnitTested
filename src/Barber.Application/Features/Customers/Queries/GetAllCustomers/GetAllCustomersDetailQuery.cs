using MediatR;

namespace Barber.Api.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersDetailQuery : IRequest<IEnumerable<GetAllCustomersDetailDto>>{
  
}