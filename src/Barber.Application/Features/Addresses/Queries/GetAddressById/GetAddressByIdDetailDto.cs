namespace Barber.Api.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdDetailDto{
  public int Id { get; set; }
  public string Street { get; set; } = string.Empty;
  public int Number { get; set; }
  public string District { get; set; } = string.Empty;
  public string City { get; set; } = string.Empty;
  public string State { get; set; } = string.Empty;
  public string CEP { get; set; } = string.Empty;
}