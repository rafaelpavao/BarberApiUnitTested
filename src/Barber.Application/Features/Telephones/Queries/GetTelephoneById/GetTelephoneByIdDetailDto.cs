using Barber.Api.Models;

namespace Barber.Api.Features.Addresses.Queries.GetTelephoneById;

public class GetTelephoneByIdDetailDto{
  public int Id { get; set; }
  public string Number { get; set; } = string.Empty;
  public TelephoneTypeDto Type { get; set; }
}