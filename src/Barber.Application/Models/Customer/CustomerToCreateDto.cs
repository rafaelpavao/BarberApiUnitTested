using Barber.Api.Entities;

namespace Barber.Api.Models;

public class CustomerToCreateDto : CustomerToManipulationDto{
  public ICollection<TelephoneToCreateDto> Telephones { get; set; } = new List<TelephoneToCreateDto>();
  public ICollection<AddressToCreateDto> Addresses { get; set; } = new List<AddressToCreateDto>();
}