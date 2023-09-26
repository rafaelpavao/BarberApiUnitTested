using Barber.Api.Entities;

namespace Barber.Api.Models;

public class CustomerWithTelephonesAndAddressesToReturnDto : CustomerToManipulationDto{
  public int Id { get; set; }
  public ICollection<TelephoneToReturnDto> Telephones { get; set; } = new List<TelephoneToReturnDto>();
  public ICollection<AddressToReturnDto> Addresses { get; set; } = new List<AddressToReturnDto>();
}