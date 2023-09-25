namespace Barber.Api.Entities;

public enum TelephoneType{ Fix, Cell }
public class Telephone{
  public int Id { get; set; }
  public string Number { get; set; } = string.Empty;
  public TelephoneType Type { get; set; }
  public int CustomerId { get; set; }
  public Customer? Customer { get; set; }
}