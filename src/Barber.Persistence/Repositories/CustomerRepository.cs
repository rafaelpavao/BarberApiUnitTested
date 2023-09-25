using AutoMapper;
using Barber.Api.DbContexts;
using Barber.Api.Entities;
using Barber.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Repositories;

public class CustomerRepository : ICustomerRepository{
  private readonly CustomerContext _context;
  private readonly IMapper _mapper;

  public CustomerRepository(CustomerContext customerContext, IMapper mapper){
    _context = customerContext ?? throw new ArgumentNullException(nameof(customerContext));
     _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
  }

  
  // Customer Commands: 
  public async Task<IEnumerable<Customer>> GetAllCustomers(){
    return await _context.Customers.OrderBy(customer => customer.Id).ToListAsync();
  }

  public async Task<Customer?> GetCustomerById(int customerId){
    return await _context.Customers.FirstOrDefaultAsync(customer => customer.Id == customerId);
  }

  public async Task<Customer?> GetCustomerWithAddressesById(int customerId){
    return await _context
      .Customers
      .Include(customer => customer.Addresses)
      .FirstOrDefaultAsync(customer => customer.Id == customerId);
  }

  public async Task<Customer?> GetCustomerWithTelephonesById(int customerId){
    return await _context
    .Customers
    .Include(customer => customer.Telephones)
    .FirstOrDefaultAsync(customer => customer.Id == customerId);
  }

  public async Task<Customer?> GetCustomerWithAddressesAndTelephonesById(int customerId){
    return await _context
      .Customers
      .Include(customer => customer.Addresses)
      .Include(customer => customer.Telephones)
      .FirstOrDefaultAsync(customer => customer.Id == customerId);
  }

  public async Task<IEnumerable<Customer>> GetAllCustomersWithTelephones(){ //Pegar os customers com os telefones junto.
    return await _context
    .Customers
    .Include(customer => customer.Telephones)
    .OrderBy(customer => customer.Id)
    .ToListAsync();
  }

  public void AddCustomer(Customer customer){
    _context.Customers.Add(customer);
  }

  public void DeleteCustomer(Customer customer){
    _context.Customers.Remove(customer);
  }

  public void DeleteAddressesAndTelephones(Customer customer){
    _context.Addresses.RemoveRange(customer.Addresses);

    _context.Telephones.RemoveRange(customer.Telephones);
  }

  public async Task<bool> SaveChangesAsync(){
    return await _context.SaveChangesAsync() > 0;
  }

  public void UpdateCustomer(Customer customer, CustomerToUpdate customerToUpdate){
    _mapper.Map(customerToUpdate, customer);
  }


  // Address Commands
  public async Task<Address?> GetAddressById(int customerId, int addressId){
    return await _context.Addresses.FirstOrDefaultAsync(address => address.Id == addressId && address.CustomerId == customerId);
  }

  public void UpdateAddress(Address address, AddressToUpdateDto addressToUpdateDto){
    _mapper.Map(addressToUpdateDto, address);
  }

  public void AddAddress(Customer customer, Address address){
    customer.Addresses.Add(address);
  }

  public void DeleteAddress(Address address){
    _context.Addresses.Remove(address);
  }


  // Telephone Commands:
  public async Task<Telephone?> GetTelephoneById(int customerId, int telephoneId){
    return await _context.Telephones.FirstOrDefaultAsync(telephone => telephone.Id == telephoneId && telephone.CustomerId == customerId);
  }

  public void AddTelephone(Customer customer, Telephone telephone){
    customer.Telephones.Add(telephone);
  }

  public void DeleteTelephone(Telephone telephone){
    _context.Telephones.Remove(telephone);
  }

  public void UpdateTelephone(Telephone telephone, TelephoneToUpdateDto telephoneToUpdateDto){
    _mapper.Map(telephoneToUpdateDto, telephone);
  }
}