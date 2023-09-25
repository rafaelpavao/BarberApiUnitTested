using Barber.Api.Models;
using MediatR;

namespace Barber.Api.Features.Customers.Commands.CreateNewCustomer;

public class CreateNewCustomerCommand : IRequest<CreateNewCustomerCommandResponse>{
  public string Name { get; set; } = string.Empty;
  public DateOnly BirthdayDate { get; set; }
  public GenderDto Gender { get; set; }
  public string CPF { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public ICollection<TelephoneToCreateDto> Telephones { get; set; } = new List<TelephoneToCreateDto>();
  public ICollection<AddressToCreateDto> Addresses { get; set; } = new List<AddressToCreateDto>();
}