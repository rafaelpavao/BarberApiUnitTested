using MediatR;

namespace Barber.Api.Features.Addresses.Queries.GetTelephoneById;

public class GetTelephoneByIdDetailQuery : IRequest<GetTelephoneByIdDetailDto>{
  public int CustomerId { get; set; }
  public int TelephoneId { get; set; }
}