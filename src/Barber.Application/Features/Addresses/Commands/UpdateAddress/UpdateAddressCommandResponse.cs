namespace Barber.Api.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommandResponse
{
    public bool IsSuccessful;
    public Dictionary<string, string[]> Errors {get; set;}

    public UpdateAddressCommandResponse(){
      IsSuccessful = true;

      Errors = new Dictionary<string, string[]>();
    }
}