using AutoMapper;
using MediatR;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using FluentValidation;

namespace Barber.Api.Features.Addresses.Commands.UpdateAddress;

// O primeiro parâmetro é o tipo da mensagem
// O segundo parâmetro é o tipo que se espera receber.
public class UpdateAddressCommandHandler: IRequestHandler<UpdateAddressCommand, UpdateAddressCommandResponse>{
  private readonly ICustomerRepository _customerRepository;
  private readonly IMapper _mapper;
  public readonly IValidator<UpdateAddressCommand> _validator;

  public UpdateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateAddressCommand> validator){
    _customerRepository = customerRepository;
    _mapper = mapper;
    _validator = validator;
  }

  public async Task<UpdateAddressCommandResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken){
    UpdateAddressCommandResponse updateAddressCommandResponse = new();

    var validationResult = _validator.Validate(request);

    if(!validationResult.IsValid){
      foreach (var error in validationResult.ToDictionary()) {
        updateAddressCommandResponse.Errors.Add(error.Key, error.Value);
      }

      updateAddressCommandResponse.IsSuccessful = false;

      return updateAddressCommandResponse;
    }


    var addressFromDatabase = await _customerRepository.GetAddressById(request.CustomerId, request.Id);

    if(addressFromDatabase == null){
      updateAddressCommandResponse.IsSuccessful = false;

      return updateAddressCommandResponse;
    }

    _mapper.Map(request, addressFromDatabase);
    await _customerRepository.SaveChangesAsync();

    return updateAddressCommandResponse;
  }
}