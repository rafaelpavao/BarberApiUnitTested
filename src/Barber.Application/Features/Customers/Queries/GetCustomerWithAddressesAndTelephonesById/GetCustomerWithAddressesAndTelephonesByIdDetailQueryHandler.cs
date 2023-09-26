using AutoMapper;
using MediatR;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Customers.Queries.GetCustomerWithAddressesAndTelephonesById;

public class GetCustomerWithAddressesAndTelephonesByIdDetailQueryHandler : IRequestHandler<GetCustomerWithAddressesAndTelephonesByIdDetailQuery, GetCustomerWithAddressesAndTelephonesByIdDetailDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerWithAddressesAndTelephonesByIdDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerWithAddressesAndTelephonesByIdDetailDto> Handle(GetCustomerWithAddressesAndTelephonesByIdDetailQuery request, CancellationToken cancellationToken)
    {
      var customerFromDatabase = await _customerRepository.GetCustomerWithAddressesAndTelephonesById(request.CustomerId);

      return _mapper.Map<GetCustomerWithAddressesAndTelephonesByIdDetailDto>(customerFromDatabase);
    }
}