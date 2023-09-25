using AutoMapper;
using MediatR;
using Barber.Api.Entities;
using Barber.Api.Repositories;
using FluentValidation;

namespace Barber.Api.Features.Customers.Commands.CreateNewCustomer;

public class CreateNewCustomerCommandHandler: IRequestHandler<CreateNewCustomerCommand, CreateNewCustomerCommandResponse>{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    public readonly IValidator<CreateNewCustomerCommand> _validator;

    public CreateNewCustomerCommandHandler(ICustomerRepository customerRepository, IMapper mapper, IValidator<CreateNewCustomerCommand> validator){
        _customerRepository = customerRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<CreateNewCustomerCommandResponse> Handle(CreateNewCustomerCommand request, CancellationToken cancellationToken){
        CreateNewCustomerCommandResponse createNewCustomerCommandResponse = new();
        
        var validationResult = _validator.Validate(request);
        
        if(!validationResult.IsValid){
            foreach (var error in validationResult.ToDictionary()) {
                createNewCustomerCommandResponse.Errors.Add(error.Key, error.Value);
            }

            createNewCustomerCommandResponse.IsSuccessful = false;

            return createNewCustomerCommandResponse;
        }

        var customerToAddInDatabase = _mapper.Map<Customer>(request);

        _customerRepository.AddCustomer(customerToAddInDatabase);
        await _customerRepository.SaveChangesAsync();

        createNewCustomerCommandResponse.Customer =  _mapper.Map<CreateNewCustomerCommandDto>(customerToAddInDatabase);

        return createNewCustomerCommandResponse;
    }
} 