using Barber.Api.Entities;
using Barber.Api.Models;

namespace Barber.Api.Repositories;

public interface ICustomerRepository{
  Task<IEnumerable<Customer>> GetAllCustomers();
  Task<Customer?> GetCustomerById(int customerId);
  Task<Customer?> GetCustomerWithAddressesById(int customerId);
  Task<Customer?> GetCustomerWithTelephonesById(int customerId);
  Task<Customer?> GetCustomerWithAddressesAndTelephonesById(int customerId);
  Task<IEnumerable<Customer>> GetAllCustomersWithTelephones();
  void UpdateCustomer(Customer customer, CustomerToUpdate customerToUpdate);
  void AddCustomer(Customer customer);
  void DeleteCustomer(Customer customer);
  void DeleteAddressesAndTelephones(Customer customer);
  Task<Address?> GetAddressById(int customerId, int addressId);
  void UpdateAddress(Address address, AddressToUpdateDto addressToUpdateDto);
  void AddAddress(Customer customer, Address address);
  void DeleteAddress(Address address);
  Task<Telephone?> GetTelephoneById(int customerId, int telephoneId);
  void AddTelephone(Customer customer, Telephone telephone);
  void DeleteTelephone(Telephone telephone);
  void UpdateTelephone(Telephone telephone, TelephoneToUpdateDto telephoneToUpdateDto);
  Task<bool> SaveChangesAsync();
}