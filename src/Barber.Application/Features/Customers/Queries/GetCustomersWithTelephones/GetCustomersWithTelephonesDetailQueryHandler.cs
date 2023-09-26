using AutoMapper;
using MediatR;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Customers.Queries.GetCustomersWithTelephones;

public class GetCustomersWithTelephonesDetailQueryHandler : IRequestHandler<GetCustomersWithTelephonesDetailQuery, IEnumerable<GetCustomersWithTelephonesDetailDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomersWithTelephonesDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCustomersWithTelephonesDetailDto>> Handle(GetCustomersWithTelephonesDetailQuery request, CancellationToken cancellationToken)
    {
        var customersFromDatabase = await _customerRepository.GetAllCustomersWithTelephones();

        return _mapper.Map<IEnumerable<GetCustomersWithTelephonesDetailDto>>(customersFromDatabase);
    }
}