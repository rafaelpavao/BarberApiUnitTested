using AutoMapper;
using Barber.Api.Entities;
using Barber.Api.Features.Customers.Commands.CreateNewCustomer;
using Barber.Api.Features.Customers.Commands.UpdateCustomer;
using Barber.Api.Features.Customers.Queries.GetAllCustomers;
using Barber.Api.Features.Customers.Queries.GetCustomerById;
using Barber.Api.Features.Customers.Queries.GetCustomersWithTelephones;
using Barber.Api.Features.Customers.Queries.GetCustomerWithAddressesAndTelephonesById;
using Barber.Api.Models;

namespace Barber.Api.Profiles;

public class CustomerProfile : Profile{
  public CustomerProfile(){
    CreateMap<Customer, CustomerToReturnDto>().ReverseMap();
    CreateMap<Customer, CustomerWithTelephonesAndAddressesToReturnDto>().ReverseMap();
    CreateMap<CustomerToCreateDto, Customer>().ReverseMap();
    CreateMap<CustomerToCreateDto, CustomerWithTelephonesAndAddressesToReturnDto>().ReverseMap();
    CreateMap<CustomerWithTelephonesAndAddressesToReturnDto, Customer>().ReverseMap();
    CreateMap<CustomerToUpdate, Customer>().ReverseMap();
    CreateMap<Customer, CustomerWithTelephonesToReturnDto>().ReverseMap();
    
    CreateMap<Gender, GenderDto>().ReverseMap();

    //CQRS

    // Queries:
    CreateMap<Customer, GetAllCustomersDetailDto>().ReverseMap();
    CreateMap<Customer, GetCustomerByIdDetailDto>().ReverseMap();
    CreateMap<Customer, GetCustomersWithTelephonesDetailDto>().ReverseMap();
    CreateMap<Customer, GetCustomerWithAddressesAndTelephonesByIdDetailDto>().ReverseMap();

    // Commands:
    CreateMap<Customer, CreateNewCustomerCommand>().ReverseMap();
    CreateMap<Customer, CreateNewCustomerCommandDto>().ReverseMap();
    CreateMap<Customer, UpdateCustomerCommand>().ReverseMap();
  }
}