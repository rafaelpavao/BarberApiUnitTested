using AutoMapper;
using MediatR;
using Barber.Api.Repositories;

namespace Barber.Api.Features.Addresses.Queries.GetTelephoneById;

public class GetTelephoneByIdDetailQueryHandler : IRequestHandler<GetTelephoneByIdDetailQuery, GetTelephoneByIdDetailDto>
{
  private readonly ICustomerRepository _customerRepository;
  private readonly IMapper _mapper;

  public GetTelephoneByIdDetailQueryHandler(ICustomerRepository customerRepository, IMapper mapper){
    _customerRepository = customerRepository;
    _mapper = mapper;
  }

  public async Task<GetTelephoneByIdDetailDto> Handle(GetTelephoneByIdDetailQuery request, CancellationToken cancellationToken){
    var telephoneFromDatabase = await _customerRepository.GetTelephoneById(request.CustomerId, request.TelephoneId);

    if(telephoneFromDatabase == null) return null!;

    return _mapper.Map<GetTelephoneByIdDetailDto>(telephoneFromDatabase);
  }
}