using AutoMapper;
using Barber.Api.Entities;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using Barber.Api.Features.Addresses.Commands.UpdateAddress;
using Barber.Api.Features.Addresses.Queries.GetAddressById;
using Barber.Api.Models;

namespace Barber.Api.Profiles;

public class AddressProfile : Profile{
  public AddressProfile(){
    CreateMap<Address, AddressToReturnDto>().ReverseMap();
    CreateMap<AddressToCreateDto, Address>().ReverseMap();
    CreateMap<AddressToCreateDto, AddressToReturnDto>().ReverseMap();
    CreateMap<Address, AddressToUpdateDto>().ReverseMap();

    //CQRS
    //Queries
    CreateMap<Address, GetAddressByIdDetailDto>().ReverseMap();

    //Commands
    CreateMap<Address, CreateAddressCommand>().ReverseMap();
    CreateMap<Address, CreateAddressCommandDto>().ReverseMap();
    CreateMap<Address, UpdateAddressCommand>().ReverseMap();
  }
}