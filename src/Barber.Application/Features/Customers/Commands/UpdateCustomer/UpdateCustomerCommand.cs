using Barber.Api.Models;
using MediatR;

namespace Barber.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<UpdateCustomerCommandResponse>{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public DateOnly BirthdayDate { get; set; }
  public GenderDto Gender { get; set; }
  public string CPF { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}