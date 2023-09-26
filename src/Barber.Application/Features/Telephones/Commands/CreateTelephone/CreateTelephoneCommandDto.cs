using Barber.Api.Models;

namespace Barber.Api.Features.Addresses.Commands.CreateTelephone;

public class CreateTelephoneCommandDto{
  public int Id { get; set; }
  public string Number { get; set; } = string.Empty;
  public TelephoneTypeDto Type { get; set; }
}