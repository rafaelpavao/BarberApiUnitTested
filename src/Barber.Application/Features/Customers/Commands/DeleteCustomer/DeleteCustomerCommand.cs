using Barber.Api.Models;
using MediatR;

namespace Barber.Api.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand : IRequest<bool>{
  public int CustomerId { get; set; }
}