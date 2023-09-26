namespace Barber.Api.Features.Addresses.Commands.CreateTelephone;

public class CreateTelephoneCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public CreateTelephoneCommandDto Telephone {get; set;} = default!;

    public CreateTelephoneCommandResponse(){
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}