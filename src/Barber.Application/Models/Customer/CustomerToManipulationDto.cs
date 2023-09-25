namespace Barber.Api.Models;

public enum GenderDto{ Masc, Fem }

public abstract class CustomerToManipulationDto{
  public string Name { get; set; } = string.Empty;
  public DateOnly BirthdayDate { get; set; }
  public GenderDto Gender { get; set; }
  public string CPF { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}