using AutoMapper;
using MediatR;
using Barber.Api.Entities;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommandHandler: IRequestHandler<DeleteAddressCommand, bool>
{
  private readonly ICustomerRepository _customerRepository;

  public DeleteAddressCommandHandler(ICustomerRepository customerRepository){
    _customerRepository = customerRepository;
  }

  public async Task<bool> Handle(DeleteAddressCommand request, CancellationToken cancellationToken){
    var addressFromDatabase = await _customerRepository.GetAddressById(request.CustomerId, request.AddressId);

    if(addressFromDatabase == null) return false;

    _customerRepository.DeleteAddress(addressFromDatabase);
    await _customerRepository.SaveChangesAsync();

    return true;
  }
}