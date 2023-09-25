using AutoMapper;
using MediatR;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Addresses.Queries.GetAddressById;

public class GetAddressByIdDetailQueryHandler : IRequestHandler<GetAddressByIdDetailQuery, GetAddressByIdDetailDto>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IMapper _mapper;

  public GetAddressByIdDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper){
    _customerRepository = customerRepository;
    _mapper = mapper;
  }

  public async Task<GetAddressByIdDetailDto> Handle(GetAddressByIdDetailQuery request, CancellationToken cancellationToken){
    var addressFromDatabase = await _customerRepository.GetAddressById(request.CustomerId, request.AddressId);

    return _mapper.Map<GetAddressByIdDetailDto>(addressFromDatabase);
  }
}