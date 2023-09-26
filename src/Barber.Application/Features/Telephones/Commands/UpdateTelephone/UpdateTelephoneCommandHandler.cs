using AutoMapper;
using MediatR;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using FluentValidation;

namespace Barber.Api.Features.Addresses.Commands.UpdateTelephone;

public class UpdateTelephoneCommandHandler: IRequestHandler<UpdateTelephoneCommand, UpdateTelephoneCommandResponse>{
  private readonly ICustomerRepository _customerRepository;
  private readonly IMapper _mapper;
   public readonly IValidator<UpdateTelephoneCommand> _validator;

  public UpdateTelephoneCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<UpdateTelephoneCommand> validator){
    _customerRepository = customerRepository;
    _mapper = mapper;
    _validator = validator;
  }

  public async Task<UpdateTelephoneCommandResponse> Handle(UpdateTelephoneCommand request, CancellationToken cancellationToken){
    UpdateTelephoneCommandResponse updateTelephoneCommandResponse = new();

     var validationResult = _validator.Validate(request);

    if(!validationResult.IsValid){
      foreach (var error in validationResult.ToDictionary()) {
        updateTelephoneCommandResponse.Errors.Add(error.Key, error.Value);
      }

      updateTelephoneCommandResponse.IsSuccessful = false;

      return updateTelephoneCommandResponse;
    }

    var telephoneFromDatabase = await _customerRepository.GetTelephoneById(request.CustomerId, request.Id);

    if(telephoneFromDatabase == null){
      updateTelephoneCommandResponse.IsSuccessful = false;

      return updateTelephoneCommandResponse;
    }

    _mapper.Map(request, telephoneFromDatabase);
    await _customerRepository.SaveChangesAsync();

    return updateTelephoneCommandResponse;
  }
}