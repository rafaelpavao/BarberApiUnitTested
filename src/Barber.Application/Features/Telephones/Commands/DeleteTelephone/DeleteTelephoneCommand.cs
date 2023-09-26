using MediatR;

namespace Barber.Api.Features.Addresses.Commands.DeleteTelephone;

public class DeleteTelephoneCommand : IRequest<bool>{
  public int CustomerId { get; set; }
  public int TelephoneId { get; set; }
}