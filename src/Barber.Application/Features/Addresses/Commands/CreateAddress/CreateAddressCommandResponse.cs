namespace Barber.Api.Features.Addresses.Commands.CreateAddress;

public class CreateAddressCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public CreateAddressCommandDto Address {get; set;} = default!;

    public CreateAddressCommandResponse(){
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}