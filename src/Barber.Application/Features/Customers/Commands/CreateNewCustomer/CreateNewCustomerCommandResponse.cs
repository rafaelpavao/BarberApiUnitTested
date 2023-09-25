namespace Barber.Api.Features.Customers.Commands.CreateNewCustomer;

public class CreateNewCustomerCommandResponse
{
    public bool IsSuccessful;

    public Dictionary<string, string[]> Errors {get; set;}

    public CreateNewCustomerCommandDto Customer {get; set;} = default!;

    public CreateNewCustomerCommandResponse(){
        IsSuccessful = true;

        Errors = new Dictionary<string, string[]>();
    }
}