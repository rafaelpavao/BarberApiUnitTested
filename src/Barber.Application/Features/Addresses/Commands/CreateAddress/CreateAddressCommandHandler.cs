using AutoMapper;
using MediatR;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using FluentValidation;

namespace Barber.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandHandler: IRequestHandler<CreateAddressCommand, CreateAddressCommandResponse>{
  private readonly ICustomerRepository _customerRepository;
  private readonly IMapper _mapper;
  public readonly IValidator<CreateAddressCommand> _validator;

  public CreateAddressCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateAddressCommand> validator){
    _customerRepository = customerRepository;
    _mapper = mapper;
    _validator = validator;
  }

  public async Task<CreateAddressCommandResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken){
    CreateAddressCommandResponse  createAddressCommandResponse =  new();

    var validationResult = _validator.Validate(request);

    if(!validationResult.IsValid){
      foreach (var error in validationResult.ToDictionary()){
        createAddressCommandResponse.Errors.Add(error.Key, error.Value);
      }

      createAddressCommandResponse.IsSuccessful = false;

      return createAddressCommandResponse;
    }

    var customerFromDatabase = await _customerRepository.GetCustomerWithAddressesById(request.CustomerId);

    if(customerFromDatabase == null) return null!;

    var addressToAdd = _mapper.Map<Address>(request);

    _customerRepository.AddAddress(customerFromDatabase, addressToAdd);
    await _customerRepository.SaveChangesAsync();

    createAddressCommandResponse.Address = _mapper.Map<CreateAddressCommandDto>(addressToAdd);

    return createAddressCommandResponse;
  }
}