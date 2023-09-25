namespace Barber.Api.Models;

public enum TelephoneTypeDto{ Fix, Cell }

public abstract class TelephoneForManipulationDto{
  public string Number { get; set; } = string.Empty;
  public TelephoneTypeDto Type { get; set; }
}