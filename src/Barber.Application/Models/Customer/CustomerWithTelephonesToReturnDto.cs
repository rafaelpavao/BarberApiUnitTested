namespace Barber.Api.Models;

public class CustomerWithTelephonesToReturnDto : CustomerToManipulationDto{
  public int Id { get; set; }
  public ICollection<TelephoneToReturnDto> Telephones { get; set; } = new List<TelephoneToReturnDto>();
}