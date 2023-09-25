using MediatR;

namespace Barber.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommand : IRequest<CreateAddressCommandResponse>{
  public string Street { get; set; } = string.Empty;
  public int Number { get; set; }
  public string District { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string CEP { get; set; } = string.Empty;
  public int CustomerId { get; set; }
}