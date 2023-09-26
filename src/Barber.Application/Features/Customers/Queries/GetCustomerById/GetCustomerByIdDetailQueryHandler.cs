using AutoMapper;
using MediatR;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdDetailQueryHandler : IRequestHandler<GetCustomerByIdDetailQuery, GetCustomerByIdDetailDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<GetCustomerByIdDetailDto> Handle(GetCustomerByIdDetailQuery request, CancellationToken cancellationToken)
    {
      var customerFromDatabase = await _customerRepository.GetCustomerById(request.CustomerId);

      return _mapper.Map<GetCustomerByIdDetailDto>(customerFromDatabase);
    }
}