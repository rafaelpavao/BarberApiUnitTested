using Barber.Api.Models;

namespace Barber.Api.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdDetailDto{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public DateOnly BirthdayDate { get; set; }
  public GenderDto Gender { get; set; }
  public string CPF { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}