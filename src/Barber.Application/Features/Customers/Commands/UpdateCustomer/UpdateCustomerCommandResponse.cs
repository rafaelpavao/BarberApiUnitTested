namespace Barber.Api.Features.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public UpdateCustomerCommandResponse(){
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}