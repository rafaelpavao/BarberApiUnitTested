using MediatR;

namespace Barber.Api.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdDetailQuery : IRequest<GetAddressByIdDetailDto>{
  public int CustomerId { get; set; }
  public int AddressId { get; set; }
}