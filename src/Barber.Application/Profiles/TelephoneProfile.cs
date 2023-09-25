using AutoMapper;
using Barber.Api.Entities;
using Barber.Api.Features.Addresses.Commands.CreateTelephone;
using Barber.Api.Features.Addresses.Commands.UpdateTelephone;
using Barber.Api.Features.Addresses.Queries.GetTelephoneById;
using Barber.Api.Models;

namespace Barber.Api.Profiles;

public class TelephoneProfile : Profile{
  public TelephoneProfile(){
    CreateMap<Telephone, TelephoneToReturnDto>().ReverseMap();
    CreateMap<TelephoneToCreateDto, TelephoneToReturnDto>().ReverseMap();
    CreateMap<TelephoneToCreateDto, Telephone>().ReverseMap();
    CreateMap<TelephoneToUpdateDto, Telephone>().ReverseMap();
    CreateMap<TelephoneTypeDto, TelephoneType>();

    //CQRS
    //Queries
    CreateMap<Telephone, GetTelephoneByIdDetailDto>().ReverseMap();

    //Commands
    CreateMap<Telephone, CreateTelephoneCommand>().ReverseMap();
    CreateMap<Telephone, CreateTelephoneCommandDto>().ReverseMap();
    CreateMap<Telephone, UpdateTelephoneCommand>().ReverseMap();
  }
}