using MediatR;

namespace Barber.Api.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommand : IRequest<bool>{
  public int CustomerId { get; set; }
  public int AddressId { get; set; }
}