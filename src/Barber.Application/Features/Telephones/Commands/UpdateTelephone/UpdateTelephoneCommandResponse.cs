namespace Barber.Api.Features.Addresses.Commands.UpdateTelephone;

public class UpdateTelephoneCommandResponse
{
    public bool IsSuccessful;
    public Dictionary<string, string[]> Errors {get; set;}

    public UpdateTelephoneCommandResponse(){
      IsSuccessful = true;

      Errors = new Dictionary<string, string[]>();
    }
}