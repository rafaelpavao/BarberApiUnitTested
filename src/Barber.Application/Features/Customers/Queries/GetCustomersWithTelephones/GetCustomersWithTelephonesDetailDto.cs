using Barber.Api.Models;

namespace Barber.Api.Features.Customers.Queries.GetCustomersWithTelephones;

public class GetCustomersWithTelephonesDetailDto{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public DateOnly BirthdayDate { get; set; }
  public GenderDto Gender { get; set; }
  public string CPF { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public ICollection<TelephoneToReturnDto> Telephones { get; set; } = new List<TelephoneToReturnDto>();
}