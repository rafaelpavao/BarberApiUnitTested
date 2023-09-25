using Barber.Api.Models;
using MediatR;

namespace Barber.Api.Features.Addresses.Commands.CreateTelephone;

public class CreateTelephoneCommand : IRequest<CreateTelephoneCommandResponse>{
  public string Number { get; set; } = string.Empty;
  public TelephoneTypeDto Type { get; set; }
  public int CustomerId { get; set; }
}