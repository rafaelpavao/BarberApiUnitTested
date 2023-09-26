using AutoMapper;
using MediatR;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Customers.Queries.GetAllCustomers;

public class GetAllCustomersDetailQueryHandler : IRequestHandler<GetAllCustomersDetailQuery, IEnumerable<GetAllCustomersDetailDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomersDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAllCustomersDetailDto>> Handle(GetAllCustomersDetailQuery request, CancellationToken cancellationToken)
    {
        var customersFromDatabase = await _customerRepository.GetAllCustomers();

        return _mapper.Map<IEnumerable<GetAllCustomersDetailDto>>(customersFromDatabase);
    }
}