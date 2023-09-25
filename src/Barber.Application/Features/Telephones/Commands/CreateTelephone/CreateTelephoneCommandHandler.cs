using AutoMapper;
using MediatR;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using FluentValidation;

namespace Barber.Api.Features.Addresses.Commands.CreateTelephone;

public class CreateTelephoneCommandHandler: IRequestHandler<CreateTelephoneCommand, CreateTelephoneCommandResponse>{
  private readonly ICustomerRepository _customerRepository;
  private readonly IMapper _mapper;
  public readonly IValidator<CreateTelephoneCommand> _validator;

  public CreateTelephoneCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateTelephoneCommand> validator){
    _customerRepository = customerRepository;
    _mapper = mapper;
    _validator = validator;
  }

  public async Task<CreateTelephoneCommandResponse> Handle(CreateTelephoneCommand request, CancellationToken cancellationToken){
    CreateTelephoneCommandResponse createTelephoneCommandResponse = new();

    var validationResult = _validator.Validate(request);

    if(!validationResult.IsValid){
      foreach (var error in validationResult.ToDictionary()){
        createTelephoneCommandResponse.Errors.Add(error.Key, error.Value);
      }

      createTelephoneCommandResponse.IsSuccessful = false;

      return createTelephoneCommandResponse;
    }

    var customerFromDatabase = await _customerRepository.GetCustomerWithTelephonesById(request.CustomerId);

    if(customerFromDatabase == null){
      createTelephoneCommandResponse.IsSuccessful = false;

      return createTelephoneCommandResponse;
    }

    var telephoneToAdd = _mapper.Map<Telephone>(request);

    _customerRepository.AddTelephone(customerFromDatabase, telephoneToAdd);
    await _customerRepository.SaveChangesAsync();

    createTelephoneCommandResponse.Telephone = _mapper.Map<CreateTelephoneCommandDto>(telephoneToAdd);

    return createTelephoneCommandResponse;
  }
}