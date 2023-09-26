namespace Barber.Api.Models;

public abstract class AddressToManipulationDto{
  public string Street { get; set; } = string.Empty;
  public int Number { get; set; }
  public string District { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string CEP { get; set; } = string.Empty;
}